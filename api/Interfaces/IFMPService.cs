namespace api.Interfaces
{
    public interface IFPMService
    {
        Task<Stock?> FindStockBySymbolAsync(string symbol);
    }
}