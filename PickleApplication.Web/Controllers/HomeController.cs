using PickleApplication.BusinessLayer.Services;
using PickleApplication.BusinessLayer.Utils;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILinkService linkService, IProductService productService)
        {
            _linkService = linkService;
            _productService = productService;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["Categories"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Category);
            ViewData["Contents"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Content);
            ViewData["CarouselProducts"] = await _productService.GetProductOrderByIdDescOrTake(5);
            return View();
        }

        private readonly ILinkService _linkService;
        private readonly IProductService _productService;
    }
}