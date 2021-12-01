using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromotionEngineLib.Prices;

namespace PromotionEngineLib.Promotions
{
    public class VolumePromotion : Promotion, IPromotion
    {
        public char SKUId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        bool IPromotion.Apply(List<IPricingItem> PricingItems, IPriceQuery PriceQuery)
        {
            bool result = false;
            if (PricingItems.FindAll(j => j.SKUId == SKUId).Sum(j => j.Count) >= Count)
            {
                RemoveNItemsOfSKU(PricingItems, SKUId, Count);

                PricingItems.Add(new PricingItem()
                {
                    Description = $"{ Count } items of { SKUId } promotion",
                    Count = 1,
                    Price = Price,
                    SKUId = ' '
                });

                result = true;
            }
            return result;
        }
    }
}
