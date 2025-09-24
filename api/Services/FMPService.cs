using api.Interfaces;
using Microsoft.Extensions.Configuration;

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
                }
            }
            catch (Exception e)
            { 
            }
        }
    }
}