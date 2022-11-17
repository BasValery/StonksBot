using StonksBot.DTO;

namespace StonksBot.Interfaces
{
    public interface IMarketInfoApiService
    {
        Task<StockInfoDto> GetStockInfo(string tickerName, string? marketName = null);
    }
}
