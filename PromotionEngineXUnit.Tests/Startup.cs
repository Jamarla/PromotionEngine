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
                .AddSingleton<SUT.Carts.ICartQuery, TestData.TestCartQuery>()
                .AddSingleton<SUT.Prices.IPriceQuery, TestData.TestPriceQuery>()
                .AddSingleton<SUT.Promotions.IVolumePromotionCreator, SUT.Promotions.VolumePromotionCreator>()
                .AddSingleton<SUT.Promotions.IBundlePromotionCreator, SUT.Promotions.BundlePromotionCreator>()
                .AddSingleton<SUT.Promotions.IDiscountPromotionCreator, SUT.Promotions.DiscountPromotionCreator>();
        }
    }
}
