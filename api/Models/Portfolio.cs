using api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
	[Table("Portfolios")]
	public class Portfolio
	{
		public int StockId { get; set; }
		public string AppUserId { get; set; }
		public Stock Stock { get; set; }
		public AppUser AppUser { get; set; }
	}
}