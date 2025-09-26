using api.Models;

namespace api.Interfaces
{
    public interface IFMPService
    {
        Task<Stock?> GetStockBySymbol(string symbol);
    }
}