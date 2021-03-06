using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotionEngineLib.Prices;

namespace PromotionEngineLib.Promotions
{
    public interface IPromotion
    {
        internal bool Apply(List<IPricingItem> PricingItems, IPriceQuery PriceQuery);
    }

    public abstract class Promotion
    {
        internal void RemoveNItemsOfSKU(List<IPricingItem> PricingItems, char SKUId, int n)
        {
            for (int i = PricingItems.Count - 1; i >= 0; i--)
            {
                var pricingItem = PricingItems[i];
                if (pricingItem.SKUId != SKUId)
                    continue;

                var cnt = Math.Min(n, pricingItem.Count);
                pricingItem.Count -= cnt;
                if (pricingItem.Count == 0)
                    PricingItems.RemoveAt(i);
                n -= cnt;
                if (n == 0)
                    break;
            }
        }
    }
}
