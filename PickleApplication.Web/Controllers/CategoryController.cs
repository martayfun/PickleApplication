using PickleApplication.BusinessLayer.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryController(IProductService productService, ILinkService linkService)
        {
            _productService = productService;
            _linkService = linkService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            ViewData["Categories"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Category);
            ViewData["Contents"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Content);
            return View(await _productService.GetProductsByCategoryId(id));
        }
        
        private readonly IProductService _productService;
        private readonly ILinkService _linkService;
    }
}