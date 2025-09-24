using api.Interfaces;
using Microsoft.Extensions.Configuration;
using api.Dtos.Stock;
using api.Mappers;

namespace api.Services
{
    public class FMPService : IFMPService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public FMPService(HttpClient httpClient, IConfiguration config) 
        {
            _config = config;
        }

        public async Task<Stock> GetStockBySymbol(string symbol)
        {
            try
            {
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_config[¡±FMPKey¡±]}")
                
                if result.IsSuccessStatusCode{
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content)
                    var stock = tasks[0];
                    if (stock != null) 
                    {
                        return stock.ToStockFromFMP();
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
                return null;
            }
        }
    }
}