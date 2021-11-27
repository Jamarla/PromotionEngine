using System;
using System.Collections.Generic;

namespace PromotionEngineLib
{
    public class PromotionEngine
    {
        public decimal TotalPrice(List<CartItem> CartItems, Dictionary<char, decimal> priceList)
        {
            var result = 0.0M;
            foreach (var cartItem in CartItems)
            {
                if (!priceList.TryGetValue(cartItem.SKUId, out var price))
                    throw new ArgumentException($"Unknown SKUId { cartItem.SKUId }");

                result += price;
            }
            return result;
        }
    }
}
