using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Microsoft.Identity.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class ProductImageService:IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<IEnumerable<ImageDto>>GetByProductIdAsync(int productId)
        {
            var images = await _productImageRepository.GetByProductIdAsync(productId);
            return images.Select(x => new ImageDto
            {
                Id = x.Id,
                productId = x.ProductId,
                ImageUrl=x.ImageUrl

            });

  
        }

        public async Task AddImageAsync(ImageDto dto)
        {
            var image = new ProductImage
            {
                ProductId=dto.productId,
                ImageUrl=dto.ImageUrl,

            };
            await _productImageRepository.AddAsync(image);

        }
    }
}
