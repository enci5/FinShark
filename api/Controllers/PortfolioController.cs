using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.User;
using api.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using api.Interfaces;
using api.Services;
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
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _user.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
    }
}