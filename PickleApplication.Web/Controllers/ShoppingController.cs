using Newtonsoft.Json;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using PickleApplication.BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PickleApplication.Web.Controllers
{
    public class ShoppingController : Controller
    {
        public ShoppingController(IProductService productService,
            ILinkService linkService, IRegionService regionService, ICityService cityService, IDeliveryInfoService deliveryInfoService,
            ICustomerService customerService, IOrderService orderService, ICountryService countryService)
        {
            _productService = productService;
            _linkService = linkService;
            _regionService = regionService;
            _cityService = cityService;
            _orderService = orderService;
            _countryService = countryService;
        }
        // GET: Shopping
        public async Task<ActionResult> ShoppingCard()
        {
            await GetLayoutData();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ShoppingDetail()
        {
            await GetLayoutData();
            ViewBag.Regions = (await _regionService.GetRegions()).ToDictionary(m => m.RegionId, m => m.RegionName).ToList();
            ViewBag.Cities = (await _cityService.GetCities()).ToDictionary(m => m.CityId, m => m.CityName).ToList();
            ViewBag.Countries = (await _countryService.GetCountries()).ToDictionary(m => m.CountryId, m => m.CountryName).ToList();
            var order = new Order();
            order.OrderLines = GetCookieOrderLines();
            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> ShoppingDetail(Order order)
        {
            await GetLayoutData();
            ViewBag.Regions = (await _regionService.GetRegions()).ToDictionary(m => m.RegionId, m => m.RegionName).ToList();
            ViewBag.Cities = (await _cityService.GetCities()).ToDictionary(m => m.CityId, m => m.CityName).ToList();
            ViewBag.Countries = (await _countryService.GetCountries()).ToDictionary(m => m.CountryId, m => m.CountryName).ToList();
            order.OrderLines = GetCookieOrderLines();

            if (ModelState.IsValid)
            {
                //customer create.
                //deliveryInfo create.
                //order create.
                //orderline create.

                var orderId = await _orderService.CreateOfReturnOrderId(order);
                var retOrder = await _orderService.GetOrderById(orderId);
                if (retOrder.OrderId > 0)
                {
                    //Remove Cookie
                    if (Request.Cookies["OrderLines"] != null)
                    {
                        var cookie = new HttpCookie("OrderLines");
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                        string html = GetMailHtmlBody(retOrder);
                        Common.SendMail(retOrder.Customer.Mail, "Yeni Sipariş", html);
                    }
                    return RedirectToAction("ShoppingResult", new RouteValueDictionary(new { controller = "Shopping", action = "ShoppingResult", id = retOrder.OrderCode }));
                }
            }
            return View(order);
        }

        private string GetMailHtmlBody(Order order)
        {
            return string.Format("" +
                "<h1>Siparişiniz karşıya iletilmiştir. En kısa sürede iletişime geçeceğiz.</h1>" +
                "<p> Sipariş numaranız : "+ order.OrderCode.ToString() +"</p>");
        }

        private IEnumerable<OrderLine> GetCookieOrderLines()
        {
            
            if (Request.Cookies["OrderLines"] != null && !string.IsNullOrEmpty(Request.Cookies["OrderLines"].Value))
            {
                return JsonConvert.DeserializeObject<IEnumerable<OrderLine>>(Server.UrlDecode(Request.Cookies["OrderLines"].Value));
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<ActionResult> ShoppingResult()
        {

            await GetLayoutData();
            //var order = await _orderService.GetOrderByOrderCode(orderCode);
            return View();
        }

        private async Task GetLayoutData()
        {
            ViewData["Categories"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Category);
            ViewData["Contents"] = await _linkService.GetLinksByType(BusinessLayer.Models.MainType.Content);
        }

        private readonly IProductService _productService;
        private readonly ILinkService _linkService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        private readonly IOrderService _orderService;
        private readonly ICountryService _countryService;

    }
}