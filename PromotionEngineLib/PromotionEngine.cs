using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngineLib.Carts;

namespace PromotionEngineLib
{
    public interface IPromotionEngine
    {
        decimal TotalPrice(int cartId, Dictionary<char, decimal> priceList, List<Promotions.Promotion> promotionList);
    }

    public class PromotionEngine : IPromotionEngine
    {
        private IServiceProvider _serviceProvider;

        public PromotionEngine(IServiceProvider ServiceProvider)
        {
            _serviceProvider = ServiceProvider;
        }

        public decimal TotalPrice(int cartId, Dictionary<char, decimal> priceList, List<Promotions.Promotion> promotionList)
        {
            var result = 0.0M;

            var cartQuery = _serviceProvider.GetRequiredService<ICartQuery>();
            var CartItems = cartQuery.GetItemsByCartId(cartId);

            var pricingItems = new List<PricingItem>();
            foreach (var cartItem in CartItems)
            {
                if (!priceList.TryGetValue(cartItem.SKUId, out var price))
                    throw new ArgumentException($"Unknown SKUId { cartItem.SKUId }");

                pricingItems.Add(new PricingItem
                {
                    SKUId = cartItem.SKUId,
                    Count = cartItem.Count,
                    Price = price,
                    Description = ""
                });
            }

            foreach (var promotion in promotionList)
            {
                do
                { } while (promotion.Apply(pricingItems));
            }

            result = pricingItems.Sum(j => j.Count * j.Price);

            return result;
        }
    }
}
