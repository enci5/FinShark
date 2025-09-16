using api.Models;
using api.Data;
using Microsoft.AspNetCore.Identity;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}