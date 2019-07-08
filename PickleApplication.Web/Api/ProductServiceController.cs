using PickleApplication.BusinessLayer.Models;
using PickleApplication.BusinessLayer.Services;
using PickleApplication.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PickleApplication.Web.Api
{
    public class ProductServiceController : ApiController
    {
        public ProductServiceController(IProductService productService, ILinkService linkService, IPictureService pictureService)
        {
            _productService = productService;
            _linkService = linkService;
            _pictureService = pictureService;
        }

        [HttpPost]
        [Route("api/ProductService/GetProducts")]
        public async Task<HttpResponseMessage> GetProducts()
        {
            var products = await _productService.GetProducts();
            if (products != null)
                return Request.CreateResponse(new ApiResponse<IEnumerable<Product>>(HttpStatusCode.OK, products, "Ürün bulundu.", true));
            else
                return Request.CreateResponse(new ApiResponse<string>(HttpStatusCode.OK, null, "Ürün Silinmiştir", true));
        }

        [HttpPost]
        [Route("api/ProductService/GetCategories")]
        public async Task<HttpResponseMessage> GetCategories()
        {
            var categories = await _linkService.GetLinksByType(MainType.Category);
            return Request.CreateResponse(new ApiResponse<IEnumerable<Link>>(HttpStatusCode.OK, categories, "Categori bulunmuştur.", true));
        }

        private readonly IProductService _productService;
        private readonly ILinkService _linkService;
        private readonly IPictureService _pictureService;
    }
}
