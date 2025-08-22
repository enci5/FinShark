using System;
using api.Models;
using api.Data;

namespace api.Interfaces 

{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault can be Null
        Task<Stock> CreateStockAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto);
        Tasj<Stock?> DeleteAsync(int id);
    }
}

