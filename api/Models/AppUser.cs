using Microsoft.AspNetCore.Identity;
using api.Models;

namespace api.Models
{ 
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
