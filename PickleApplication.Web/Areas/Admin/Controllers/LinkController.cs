using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class LinkController : Controller
    {
        public LinkController(ILinkService linkService)
        {
            _linkService = linkService;
        }
        [HttpGet]
        public async Task<ActionResult> All()
        {
            if (TempData["DeleteStatus"] != null) ViewBag.DeleteStatus = (bool)TempData["DeleteStatus"];
            return View(await _linkService.GetLinks());
        }

        [HttpPost]
        public async Task<ActionResult> Modify(Link link)
        {
            ViewBag.Links = (await _linkService.GetLinksByType(MainType.Category)).ToDictionary(l => l.LinkId, l => l.Name);
            if (ModelState.IsValid)
            {
                if (link.LinkId > 0)
                    ViewBag.UpdateStatus = await _linkService.LinkUpdate(link);
                else
                    ViewBag.CreateStatus = await _linkService.LinkCreate(link);
                ModelState.Clear();
                return View(new Link());
            }
            return View(link);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["DeleteStatus"] = await _linkService.LinkDeleteById(id);
            return RedirectToAction("all");
        }

        [HttpGet]
        public async Task<ActionResult> Modify(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Links = (await _linkService.GetLinksByType(MainType.Category)).Where(l => l.LinkId != id).ToDictionary(l => l.LinkId, l => l.Name);
                return View(await _linkService.GetLinkById((int)id));
            }
            else
            {
                ViewBag.Links = (await _linkService.GetLinksByType(MainType.Category)).ToDictionary(l => l.LinkId, l => l.Name);
                return View();
            }
        }

        private readonly ILinkService _linkService;
    }
}