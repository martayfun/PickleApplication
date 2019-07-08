using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class HtmlContentController : Controller
    {
        public HtmlContentController(IContentService ContentService, ILinkService linkService)
        {
            _contentService = ContentService;
            _linkService = linkService;
        }
        [HttpGet]
        public async Task<ActionResult> All()
        {
            if (TempData["DeleteStatus"] != null) ViewBag.DeleteStatus = (bool)TempData["DeleteStatus"];
            return View(await _contentService.GetContents());
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int? id)
        {

            if (id.HasValue)
            {
                var content = await _contentService.GetContentById((int)id);
                ViewBag.Links = (await _linkService.GetLinks()).Where(l => l.IsAssigned == false || l.LinkId == content.Link.LinkId).ToDictionary(d => d.LinkId, d => d.Name);
                return View(content);
            }
            else
            {
                ViewBag.Links = (await _linkService.GetLinks()).Where(l => l.IsAssigned == false).ToDictionary(d => d.LinkId, d => d.Name);
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Modify(Content Content)
        {
            if (ModelState.IsValid)
            {
                if (Content.ContentId > 0)
                    ViewBag.UpdateStatus = await _contentService.ContentUpdate(Content);
                else
                    ViewBag.CreateStatus = await _contentService.ContentCreate(Content);
                ModelState.Clear();
                ViewBag.Links = (await _linkService.GetLinks()).Where(l => l.IsAssigned == false).ToDictionary(d => d.LinkId, d => d.Name);
                return View(new Content());
            }
            ViewBag.Links = (await _linkService.GetLinks()).Where(l => l.IsAssigned == false).ToDictionary(d => d.LinkId, d => d.Name);
            return View(Content);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["DeleteStatus"] = await _contentService.ContentDeleteById(id);
            return RedirectToAction("all");
        }

        private readonly IContentService _contentService;
        private readonly ILinkService _linkService;
    }
}