using System;
using api.Models;
using api.Data;

namespace api.Interfaces 

{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
    }
}

