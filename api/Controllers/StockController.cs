using System;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SotckController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Stock>> GetAll()
        {
            var stocks = _context.Stocks.ToList();
            return stocks;
        }

        [HttpGet("{id}")]
        public ActionResult<Stock> GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null) {
                return NotFound();
            }
            return stock;
    }


