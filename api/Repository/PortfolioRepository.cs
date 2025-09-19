using api.Models;
using api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        public readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.Stock.Id,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                }).ToListAsync();
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
    }
}