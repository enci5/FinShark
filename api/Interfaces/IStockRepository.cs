using System;
using api.Models;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;

namespace api.Interfaces 

{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault can be Null
        Task<Stock> CreateStockAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExistsAsync(int id);
    }
}

