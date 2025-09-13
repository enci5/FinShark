using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.User;
using api.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using api.Interfaces;
using api.Services;

namespace api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _usermanager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                var createdUser = await _usermanager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded) 
                {
                    var roleResult = await _usermanager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded) 
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}