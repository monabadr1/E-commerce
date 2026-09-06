using Domain.Entities;
using Infrastructure.IRepository;
using Infrastructure.IService;
using Shared;

namespace Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductVariantRepository _productVariantRepository;

        public ProductService(IProductRepository productRespository, ICategoryRepository categoryRepository, IProductVariantRepository productVariantRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRespository;
            _productVariantRepository = productVariantRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var today = DateOnly.FromDateTime(DateTime.Now);

            return products.Select(p =>
            {
                var originalPrice = p.ProductVariants.Select(v => v.Price).FirstOrDefault();
                bool hasActiveDiscount = p.Discount != null && p.Discount.IsActive && p.Discount.StartDate <= today && p.Discount.EndDate >= today;
                decimal discountPercentage = hasActiveDiscount ? p.Discount!.Percentage : 0;
                decimal finalPrice = hasActiveDiscount ? originalPrice - (originalPrice * (discountPercentage / 100m)) : originalPrice;

                return new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    OriginalPrice = originalPrice,
                    Price = finalPrice,
                    DiscountPercentage = discountPercentage,
                    Images = p.ProductImages.Select(i => i.ImageUrl!).ToList()
                };
            }).ToList();
        }

        public async Task<ProductDetailsDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return null;

            var today = DateOnly.FromDateTime(DateTime.Now);
            bool hasActiveDiscount = product.Discount != null && product.Discount.IsActive && product.Discount.StartDate <= today && product.Discount.EndDate >= today;
            decimal discountPercentage = hasActiveDiscount ? product.Discount!.Percentage : 0;

            return new ProductDetailsDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                CategoryId = product.CategoryId,
                DiscountPercentage = discountPercentage,

                Price = product.ProductVariants.Select(v => v.Price).FirstOrDefault(),

                Images = product.ProductImages.Select(i => i.ImageUrl!).ToList(),
                Variants = product.ProductVariants.Select(v => new ProductVariantDto
                {
                    ProductVariantId = v.ProductVariantId,
                    ProductId = v.ProductId,
                    size = v.Size,
                    color = v.Color,
                    StockQuantity = v.StockQuantity,
                    Price = v.Price,
                }).ToList() ?? new List<ProductVariantDto>()
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            var today = DateOnly.FromDateTime(DateTime.Now);

            return products.Select(p =>
            {
                var originalPrice = p.ProductVariants.Select(v => v.Price).FirstOrDefault();
                bool hasActiveDiscount = p.Discount != null && p.Discount.IsActive && p.Discount.StartDate <= today && p.Discount.EndDate >= today;
                decimal discountPercentage = hasActiveDiscount ? p.Discount!.Percentage : 0;
                decimal finalPrice = hasActiveDiscount ? originalPrice - (originalPrice * (discountPercentage / 100m)) : originalPrice;

                return new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name,
                    OriginalPrice = originalPrice,
                    Price = finalPrice,
                    DiscountPercentage = discountPercentage,
                    Images = p.ProductImages.Select(i => i.ImageUrl!).ToList(),
                    Colors = p.ProductVariants.Select(v => v.Color!).Distinct().ToList()
                };
            }).ToList();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByMainCategoryAsync(int mainCategoryId)
        {
            var products = await _productRepository.GetByMainCategoryIdAsync(mainCategoryId);
            var today = DateOnly.FromDateTime(DateTime.Now);

            return products.Select(p =>
            {
                var originalPrice = p.ProductVariants.Select(v => v.Price).FirstOrDefault();
                bool hasActiveDiscount = p.Discount != null && p.Discount.IsActive && p.Discount.StartDate <= today && p.Discount.EndDate >= today;
                decimal discountPercentage = hasActiveDiscount ? p.Discount!.Percentage : 0;
                decimal finalPrice = hasActiveDiscount ? originalPrice - (originalPrice * (discountPercentage / 100m)) : originalPrice;

                return new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category?.Name,
                    OriginalPrice = originalPrice,
                    Price = finalPrice,
                    DiscountPercentage = discountPercentage,
                    Images = p.ProductImages.Select(i => i.ImageUrl!).ToList(),
                    Colors = p.ProductVariants.Select(c => c.Color!).Distinct().ToList()
                };
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
            var sizes = new List<string> { "S", "M", "L", "XL", "XXL" };

            foreach (var size in sizes)
            {
                var variant = new ProductVariant
                {
                    ProductId = entity.ProductId,
                    Price = product.Price,
                    Size = size,
                    Color = product.Color ?? "Default",
                    StockQuantity = 10
                };
                await _productVariantRepository.AddVariantAsync(variant);
            }
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            var existingproduct = await _productRepository.GetByIdAsync(product.ProductId);
            if (existingproduct == null)
                throw new Exception("Products not found");

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new Exception("Category not found");

            existingproduct.Name = product.Name;
            existingproduct.Description = product.Description;
            existingproduct.CategoryId = product.CategoryId;

            await _productRepository.UpdateAsync(existingproduct);
        }

        public async Task DeleteProductAsync(int id)
        {
            var existingproduct = await _productRepository.GetByIdAsync(id);
            if (existingproduct == null)
                throw new Exception("Product not found");

            await _productRepository.DeleteAsync(id);
        }
    }
}