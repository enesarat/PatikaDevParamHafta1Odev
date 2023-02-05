using System.ComponentModel.DataAnnotations;

namespace PatikaDevParamHafta1Odev.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 60, MinimumLength = 10, ErrorMessage = "Invalid name lenght!")]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 100, ErrorMessage = "Invalid description lenght!")]
        public string Description { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Invalid category name lenght!")]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [Range(minimum: 5, maximum: 500, ErrorMessage = "Invalid quantity! Product quantitiy must be between 5 to 500.")]
        public int Quantity { get; set; }

    }
}
