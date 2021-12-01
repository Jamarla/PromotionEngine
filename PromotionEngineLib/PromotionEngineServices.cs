using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PromotionEngineLib
{
    public static class PromotionEngineServices
    {
		public static IServiceCollection AddPromotionEngineServices(this IServiceCollection services)
		{
			return services
                .AddSingleton<IPromotionEngine, PromotionEngine>()
                .AddSingleton<Promotions.IVolumePromotionCreator, Promotions.VolumePromotionCreator>()
                .AddSingleton<Promotions.IBundlePromotionCreator, Promotions.BundlePromotionCreator>()
                .AddSingleton<Promotions.IDiscountPromotionCreator, Promotions.DiscountPromotionCreator>();
        }
    }
}
