using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickleApplication.BusinessLayer.Models;
using PickleApplication.DataLayer.Repository;

namespace PickleApplication.BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        public ProductService(IProductRepository productRepository, IPictureService pictureService)
        {
            _productRepository = productRepository;
            _pictureService = pictureService;
        }

        public async Task<bool> DeleteProductById(int id)
        {
            return await _productRepository.Delete(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = (await _productRepository.GetAll()).Select(p => new Product
            {
                ProductId = p.Id,
                ProductName = p.ProductName,
                ProductCode = p.ProductCode,
                Link = new Link
                {
                    LinkId = p.Link.Id,
                    Name = p.Link.Name
                },
                Star = p.Star,
                Title = p.Title,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Stock = p.Stock,
            }).ToList();
            foreach (var product in products)
            {
                product.Pictures = await _pictureService.GetPicturesByProductId(product.ProductId);
            }
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null) return null;
            return new Product
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCode = product.ProductCode,
                Link = new Link
                {
                    Name = product.Link.Name,
                    LinkId = product.Link.Id
                },
                Star = product.Star,
                Stock = product.Stock,
                Title = product.Title,
                UnitPrice = product.UnitPrice,
            };
        }

        public async Task<bool> CreateProduct(Product product)
        {
            return await _productRepository.Create(new DataLayer.Models.Product
            {
                ProductName = product.ProductName,
                ProductCode = string.Format("PR{0}", DateTime.Now.ToString("ddMMyyyyHHmm")),
                LinkId = product.Link.LinkId,
                Title = product.Title,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                Star = product.Star,
            });
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productRepository.Update(new DataLayer.Models.Product
            {
                Id = product.ProductId,
                LinkId = product.Link.LinkId,
                Description = product.Description,
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                Star = product.Star,
                Stock = product.Stock,
                Title = product.Title,
                UnitPrice = product.UnitPrice
            });
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            return (await _productRepository.GetProductsByCategoryId(id)).Select(p => new Product
            {
                ProductId = p.Id,
                ProductName = p.ProductName,
                ProductCode = p.ProductCode,
                Link = new Link
                {
                    LinkId = p.Link.Id,
                    Name = p.Link.Name
                },
                Star = p.Star,
                Title = p.Title,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Stock = p.Stock,
            }).ToList();
        }

        public async Task<IEnumerable<Product>> GetProductOrderByIdDescOrTake(int count)
        {
            var products = (await _productRepository.GetProductOrderByIdDescOrTake(count)).Select(p => new Product
            {
                ProductId = p.Id,
                ProductName = p.ProductName,
                ProductCode = p.ProductCode,
                Star = p.Star,
                Title = p.Title,
                Description = p.Description,
                UnitPrice = p.UnitPrice,
                Stock = p.Stock,
                Link = new Link
                {
                    LinkId = p.Link.Id,
                    Name = p.Link.Name,
                    Description = p.Link.Description,
                    IsActive = p.Link.IsActive,
                    IsAssigned = p.Link.IsAssigned,
                    ParentId = p.Link.ParentId,
                    Type = (MainType)p.Link.Type
                }
            }).ToList();
            foreach (var product in products)
            {
                product.Pictures = await _pictureService.GetPicturesByProductId(product.ProductId);
            }
            return products;
        }

        private readonly IProductRepository _productRepository;
        private readonly IPictureService _pictureService;
    }
}
