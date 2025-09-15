using System;
using System.ComponentModel.DataAnnotations.Schema;
using api.Models;

namespace api.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<Comment>? Comment { get; set; }
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}

