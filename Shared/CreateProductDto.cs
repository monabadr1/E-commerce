using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Product price is required.")]
        [Range(1, 1000000, ErrorMessage = "Price must be greater than 0.")]
        public int Price { get; set; }

        public string? Size { get; set; } = "Free Size";

        [Required(ErrorMessage = "Please select a valid subcategory.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid subcategory.")]
        public int CategoryId { get; set; }

        public string? Color { get; set; } = "Default";
        public int StockQuantity { get; set; } = 10;

        public List<string> Images { get; set; } = new();
    }
}