using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class AdmProductController : Controller
    {
        public AdmProductController(IProductService productService, ILinkService linkService, IPictureService pictureService)
        {
            _productService = productService;
            _linkService = linkService;
            _pictureService = pictureService;
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            if (TempData["DeleteStatus"] != null) ViewBag.DeleteStatus = (bool)TempData["DeleteStatus"];
            return View(await _productService.GetProducts());
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int? id)
        {
            ViewBag.Categories = (await _linkService.GetLinks()).Where(l => l.Type == MainType.Category).ToDictionary(d => d.LinkId, d => d.Name);
            if (id.HasValue)
                return View(await _productService.GetProductById((int)id));
            else
                return View();
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Product product)
        {
            ViewBag.Categories = (await _linkService.GetLinks()).Where(l => l.Type == MainType.Category).ToDictionary(d => d.LinkId, d => d.Name);
            if (ModelState.IsValid)
            {
                if (product.ProductId > 0)
                {
                    ViewBag.UpdateStatus = await _productService.UpdateProduct(product);
                }
                else
                {
                    ViewBag.CreateStatus = await _productService.CreateProduct(product);
                }
                ModelState.Clear();
                return View(new Product());
            }
            else
                return View(product);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var pictures = await _pictureService.GetPicturesByProductId(id);
            foreach (var picture in pictures)
            {
                var path = Path.Combine(Server.MapPath("~/Content/Pictures"), string.Format("{0}{1}", picture.PictureId, picture.Extension));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    await _pictureService.DeletePictureById(picture.PictureId);
                }
            }
            TempData["DeleteStatus"] = await _productService.DeleteProductById(id);
            return RedirectToAction("all");
        }

        [HttpGet]
        public async Task<ActionResult> Picture(int id)
        {
            return View(await _pictureService.GetPicturesByProductId(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreatePicture(HttpPostedFileBase file)
        {
            string productId = TempData["ProductId"].ToString();
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    if (!Directory.Exists(Server.MapPath("~/Content/Pictures")))
                        Directory.CreateDirectory(Server.MapPath("~/Content/Pictures"));

                    string extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString();
                    var path = Path.Combine(Server.MapPath("~/Content/Pictures"), string.Format("{0}{1}", fileName, extension));
                    file.SaveAs(path);

                    ViewBag.Status = await _pictureService.CreatePicture(new Picture
                    {
                        PictureId = fileName,
                        Extension = extension,
                        IsActive = true,
                        ProductId = Convert.ToInt32(productId)
                    });
                }
                catch (Exception)
                {
                    ViewBag.Status = false;
                }
            }
            else
            {
                ViewBag.ModelState = false;
            }
            return RedirectToAction("Picture", new RouteValueDictionary(new { controller = "AdmProduct", action = "Picture", id = productId }));
        }

        [HttpGet]
        public async Task<ActionResult> DeletePictureById(string id)
        {
            string productId = TempData["ProductId"].ToString();

            var picture = await _pictureService.GetPictureById(id);
            var path = Path.Combine(Server.MapPath("~/Content/Pictures"), string.Format("{0}{1}", picture.PictureId, picture.Extension));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                await _pictureService.DeletePictureById(picture.PictureId);
            }
            return RedirectToAction("Picture", new RouteValueDictionary(new { controller = "Product", action = "Picture", id = productId }));
        }

        private readonly IProductService _productService;
        private readonly ILinkService _linkService;
        private readonly IPictureService _pictureService;
    }
}