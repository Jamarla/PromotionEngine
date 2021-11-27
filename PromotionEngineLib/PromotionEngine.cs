using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngineLib
{
    public class PromotionEngine
    {
        public decimal TotalPrice(List<CartItem> CartItems, Dictionary<char, decimal> priceList, List<Promotions.Promotion> promotionList)
        {
            var result = 0.0M;

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
