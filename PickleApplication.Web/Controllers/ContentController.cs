using PickleApplication.BusinessLayer.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Controllers
{
    public class ContentController : Controller
    {
        public ContentController(ILinkService linkService, IContentService contentService)
        {
            _linkService = linkService;
            _contentService = contentService;
        }

        [HttpGet]
        [Route("Content/{id}")]
        public async Task<ActionResult> Index(int id)
        {
            ViewData["Categories"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Category);
            ViewData["Contents"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Content);
            return View(await _contentService.GetContentByLinkId(id));
        }

        private readonly ILinkService _linkService;
        private readonly IContentService _contentService;
    }
}