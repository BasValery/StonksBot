using Newtonsoft.Json;
using StonksBot.Configurations;
using StonksBot.DTO;
using StonksBot.Interfaces;

namespace StonksBot.Services
{
    public class MarketInfoApiService : IMarketInfoApiService
    {
        MarketApiConfig marketApiConfig;
        public MarketInfoApiService(MarketApiConfig marketApiConfig) 
        { 
            this.marketApiConfig = marketApiConfig;
        }

        public async Task<StockInfoDto> GetStockInfo(string tickerName, string? marketName = null)
        {
            using var httpClient  = new HttpClient();
            string QUERY_URL = $"{marketApiConfig.ApiUrl}/query?function=GLOBAL_QUOTE&symbol={tickerName}&apikey={marketApiConfig.ApiToken}";
            
            var result = await httpClient.GetStringAsync(QUERY_URL);
            if(result == null)
            {
                throw new ArgumentNullException("Ticker wasn't found");
            }
            

            return StockInfoDtoMapper(result);
        }

        private StockInfoDto StockInfoDtoMapper(string jsonObject)
        {
            dynamic? data = JsonConvert.DeserializeObject(jsonObject);
            if(data == null)
            {
                throw new Exception("Can't parce data from data provider");
            }
            return new StockInfoDto { 
                Price = Convert.ToDecimal(data["Global Quote"]["05. price"]),
                Ticker = data["Global Quote"]["01. symbol"]
            };
        }
    }
}
