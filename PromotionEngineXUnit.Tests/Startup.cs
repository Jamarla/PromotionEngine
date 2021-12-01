using Microsoft.Extensions.DependencyInjection;
using PromotionEngineLib;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddPromotionEngineServices()
                .AddSingleton<SUT.Carts.ICartQuery, TestData.TestCartQuery>()
                .AddSingleton<SUT.Prices.IPriceQuery, TestData.TestPriceQuery>();
        }
    }
}
