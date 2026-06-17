using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Infrastructure.Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class ProductVariantService :IProductVariantService
    {
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductRepository _productRepository;

        public ProductVariantService(IProductVariantRepository productVariantRepository, IProductRepository productRepository)
        {
            _productVariantRepository = productVariantRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductVariantDto?> GetByIdAsync(int productvariantId)
        {
            var variant=await _productVariantRepository.GetByIdAsync(productvariantId);
            if (variant == null)
                return null;
            return new ProductVariantDto
            {

                ProductVariantId = variant.ProductVariantId,
                color = variant.Color,
                Price = variant.Price,
                size = variant.Size,
                StockQuantity = variant.StockQuantity,
                ProductId = variant.ProductId,
                

            };
        }

        public async Task<IEnumerable<ProductVariantDto>> GetByProductIdAsync(int productId)
        {
            var variant= await _productVariantRepository.GetByProductIdAsync(productId);
            return variant.Select(v => new ProductVariantDto
            {
                ProductVariantId=v.ProductVariantId,
                size=v.Size,
                StockQuantity=v.StockQuantity,
                ProductId=v.ProductId,
                color = v.Color,
                Price=v.Price,
            }).ToList();


        }

       public async Task AddProductVariantAsync(CreateProductVariantDto productVariant)
        {
            var product = await _productRepository.GetByIdAsync(productVariant.ProductId);
            if (product == null)
                throw new Exception ("product not found");

            var entity = new ProductVariant
            {
                Price = productVariant.Price,
                Color = productVariant.color,
                Size = productVariant.size,
                StockQuantity=productVariant.StockQuantity,
                ProductId=productVariant.ProductId,
                
            };
            await _productVariantRepository.AddVariantAsync(entity);
        }

        public async Task UpdateProductVariantAsync(ProductVariantDto productVariant)
        {
            var variant = await _productVariantRepository.GetByIdAsync(productVariant.ProductVariantId);
            if (variant == null)
                throw new Exception("variant not found ");
            var product = await _productRepository.GetByIdAsync(productVariant.ProductId);
            if (product == null)
                throw new Exception("Product not found");
            variant.Size = productVariant.size;
            variant.Color = productVariant.color;
            variant.StockQuantity = productVariant.StockQuantity;
            variant.ProductId = productVariant.ProductId;
            variant.Price = productVariant.Price;
            await _productVariantRepository.UpdateVariantAsync(variant);  
        }

        public async Task DeleteProductVariantAsync(int productVariantId)
        {
             await _productVariantRepository.DeleteVariantAsync(productVariantId);
        }


    }
}
