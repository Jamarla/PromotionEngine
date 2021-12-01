using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngineLib.Carts;
using PromotionEngineLib.Prices;

namespace PromotionEngineLib
{
    public interface IPromotionEngine
    {
        decimal TotalPrice(int cartId, List<Promotions.IPromotion> promotionList);
    }

    public class PromotionEngine : IPromotionEngine
    {
        private IServiceProvider _serviceProvider;

        public PromotionEngine(IServiceProvider ServiceProvider)
        {
            _serviceProvider = ServiceProvider;
        }

        public decimal TotalPrice(int cartId, List<Promotions.IPromotion> promotionList)
        {
            var result = 0.0M;

            var cartQuery = _serviceProvider.GetRequiredService<ICartQuery>();
            var CartItems = cartQuery.GetItemsByCartId(cartId);
            var priceQuery = _serviceProvider.GetRequiredService<IPriceQuery>();

            var pricingItems = new List<IPricingItem>();
            foreach (var cartItem in CartItems)
            {
                var price = priceQuery.GetBySKUId(cartItem.SKUId);
                if (price == null)
                    throw new ArgumentException($"Unknown SKUId { cartItem.SKUId }");

                pricingItems.Add(new PricingItem
                {
                    SKUId = cartItem.SKUId,
                    Count = cartItem.Count,
                    Price = price.Price,
                    Description = ""
                });
            }

            foreach (var promotion in promotionList)
            {
                do
                { } while (promotion.Apply(pricingItems, priceQuery));
            }

            result = pricingItems.Sum(j => j.Count * j.Price);

            return result;
        }
    }
}
