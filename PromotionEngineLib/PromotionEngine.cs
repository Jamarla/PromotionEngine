using System;
using System.Collections.Generic;

namespace PromotionEngineLib
{
    public class PromotionEngine
    {
        public decimal TotalPrice(char[] SKUIds, Dictionary<char, decimal> priceList)
        {
            var result = 0.0M;
            foreach (var sku in SKUIds)
            {
                if (!priceList.TryGetValue(sku, out var price))
                    throw new ArgumentException($"Unknown SKUId { sku }");

                result += price;
            }
            return result;
        }
    }
}
