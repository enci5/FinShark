using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.User;
using api.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using api.Interfaces;
using api.Services;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> usermanager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _usermanager = usermanager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername() ?? string.Empty;
            var appUser = await _usermanager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToPortfolio(string Symbol)
        {
            var username = User.GetUsername() ?? string.Empty;
            var appUser = await _usermanager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(Symbol);
            if (stock == null) 
            {
                return BadRequest($"Stock with the symbol {Symbol} not found");
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Any(s => s.Symbol.ToLower() == Symbol.ToLower())) {
                return BadRequest("Stock already exists");
            }

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _portfolioRepository.CreateAsync(portfolioModel);
            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create portfolio");
            }
            return Created();
        }
    }
}