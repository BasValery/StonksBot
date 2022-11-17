using Microsoft.Extensions.Configuration;
using StonksBot.Configurations;
using StonksBot.Interfaces;
using StonksBot.Services;

namespace StonksBot.Utils
{
    public static class ServiceRegistration
    {
        public static void RegisterAppServices(this IServiceCollection services) {
            services.AddSingleton<IMarketInfoApiService>((serviceProvider) =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>()?.GetSection("MarketApiConfig").Get<MarketApiConfig>();
                if(configuration == null) {
                    throw new ArgumentNullException("Market configuration isn't exists");
                }
                return new MarketInfoApiService(configuration);
            });
        }
    }
}
