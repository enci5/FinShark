using api.Models;
using api.Data;
using Microsoft.AspNetCore.Identity;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync (Portfolio portfolio);
        Task<Portfolio> DeletePortfolioAsync(AppUser user, string symbol);
    }
}