using System;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Data;
using api.Mappers;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Repository;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _repo;

        public StockController(ApplicationDbContext context, IStockRepository stockRepo)
        {
            _context = context;
            _repo = stockRepo;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject)
        {
            var stocks = await _repo.GetAllAsync(queryObject);
            var stocksDto = stocks.Select(s => s.ToStockDto());
            
            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _repo.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public async Task<IActionResult> AddNewStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _repo.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> ChangeStock([FromRoute]int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stockModel = await _repo.UpdateAsync(id, updateDto);
            if (stockModel == null)  
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _repo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent(); // returns 204 which is thumbs up for deleted
        }
    }
}