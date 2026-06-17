using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
namespace Infrastructure.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRespository,ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRespository;
        }

       public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,  
                Images=p.ProductImages
                .Select(i=>i.ImageUrl!)
                .ToList()
                
            }).ToList();



        }
        public async Task<ProductDetailsDto?> GetProductByIdAsync(int id)
        {
           var product= await _productRepository.GetByIdAsync(id);
            if (product==null)
                return null;
            return new ProductDetailsDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description= product.Description,
                CategoryId= product.CategoryId,

                Price = product.ProductVariants
                .Select(v=>v.Price)
                .FirstOrDefault(),

                 Images = product.ProductImages
                .Select(i => i.ImageUrl!)
                .ToList(),
                Variants =product.ProductVariants.Select(
                    v=>new ProductVariantDto
                    {
                        ProductVariantId = v.ProductVariantId,
                        ProductId = v.ProductId,
                        size=v.Size,
                        color=v.Color,
                        StockQuantity=v.StockQuantity,
                        Price=v.Price,

                    }
                    
                    
                    ).ToList()?? new List<ProductVariantDto>()

            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products= await _productRepository.GetByCategoryIdAsync(categoryId);

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name= p.Name,
                Description= p.Description,
                CategoryId= p.CategoryId,
                Images = p.ProductImages
                .Select(i => i.ImageUrl!)
                .ToList(),

                Price=p.ProductVariants
                .Select(v=>v.Price)
                .FirstOrDefault(),

                Colors=p.ProductVariants
                .Select(v=>v.Color!)
                .Distinct()
                .ToList()
            }).ToList();


        }
        public async Task<IEnumerable<ProductDto>>GetProductsByMainCategoryAsync(int mainCategoryId)
        {
            var products=await _productRepository.GetByMainCategoryIdAsync(mainCategoryId);

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Images = p.ProductImages
                .Select(i => i.ImageUrl!)
                .ToList(),
                Price = p.ProductVariants
                .Select(v => v.Price)
                .FirstOrDefault(),
                Colors=p.ProductVariants
                .Select(c=>c.Color!)
                .Distinct()
                .ToList(),
            }).ToList();
        }

        public async Task AddProductAsync(CreateProductDto product)
        {
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new Exception("Category not found");
            var entity = new Product
            {
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
            };
             await _productRepository.AddAsync(entity);
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var existingproduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingproduct == null)
                throw new Exception("Prdoucts not found ");
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new Exception("category not found");

            existingproduct.Name = product.Name;
            existingproduct.Description = product.Description;
            existingproduct.CategoryId = product.CategoryId;
   

            await _productRepository.UpdateAsync(existingproduct);
        }
      public async  Task DeleteProductAsync(int id)
        {
            var existingproduct= await _productRepository.GetByIdAsync(id);
            if (existingproduct == null)
                throw new Exception("product not found");
            await _productRepository.DeleteAsync(id);
        }

    }
}
