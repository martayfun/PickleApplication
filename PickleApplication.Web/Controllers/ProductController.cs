using PickleApplication.BusinessLayer.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductController(IProductService productService, ILinkService linkService, IPictureService pictureService)
        {
            _productService = productService;
            _linkService = linkService;
            _pictureService = pictureService;
        }

        [HttpGet]
        public async Task<ActionResult> ProductList(int? id)
        {
            await GetLayoutData();
            ViewData["Id"] = id;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProductDetail(int? id)
        {
            await GetLayoutData();
            if (id.HasValue)
            {
                ViewData["Pictures"] = await _pictureService.GetPicturesByProductId((int)id);
                return View(await _productService.GetProductById((int)id));
            }
            else
            {
                return View();
            }
        }

        private async Task GetLayoutData()
        {
            ViewData["Categories"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Category);
            ViewData["Contents"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Content);
        }

        private readonly IProductService _productService;
        private readonly ILinkService _linkService;
        private readonly IPictureService _pictureService;
    }
}