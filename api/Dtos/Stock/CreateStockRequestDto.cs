using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Name cannot be greater than 10")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be greater than 10")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be greater than 10 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000)]
        public long MarketCap { get; set; }
    }
}