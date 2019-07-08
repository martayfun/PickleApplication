using PickleApplication.BusinessLayer.Services;
using PickleApplication.Web.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PickleApplication.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            if (TempData["Approval"] != null) ViewBag.Approval = (bool)TempData["Approval"];
            if (TempData["Cancel"] != null) ViewBag.Cancel = (bool)TempData["Cancel"];
            return View(await _orderService.GetOrders());
        }

        [HttpGet]
        public async Task<ActionResult> Approval(int id)
        {
            TempData["Approval"] = await _orderService.OrderApproval(id);
            return RedirectToAction("all");
        }

        [HttpGet]
        public async Task<ActionResult> Cancel(int id)
        {
            TempData["Cancel"] = await _orderService.OrderCancel(id);
            return RedirectToAction("all");
        }

        private readonly IOrderService _orderService;
    }
}