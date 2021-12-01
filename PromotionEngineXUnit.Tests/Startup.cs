using Microsoft.Extensions.DependencyInjection;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<SUT.IPromotionEngine, SUT.PromotionEngine>()
                .AddSingleton<SUT.Carts.ICartQuery, TestData.TestCartQuery>();
        }
    }
}
