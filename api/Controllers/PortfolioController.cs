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
using Azure.Identity;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;

        public PortfolioController(UserManager<AppUser> usermanager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository, IFMPService _fmpService)
        {
            _usermanager = usermanager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = _fmpService;
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
        public async Task<IActionResult> AddToPortfolio(string symbol)
        {
            var username = User.GetUsername() ?? string.Empty;
            var appUser = await _usermanager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fmpService.GetStockBySymbol(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exist");
                }
                else
                {
                    await _stockRepository.CreateStockAsync(stock);
                }
            }
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

            if (userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower())) {
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

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemoveFromPortfolio(string symbol)
        {
            var userName = User.GetUsername() ?? string.Empty;
            var appUser = await _usermanager.FindByNameAsync(userName);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();
            if (filteredStock.Count() == 1)
            {
                await _portfolioRepository.DeletePortfolioAsync(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }
            return Ok();
        }
    }
}