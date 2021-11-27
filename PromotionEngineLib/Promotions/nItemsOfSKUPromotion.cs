using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib.Promotions
{
    public class nItemsOfSKUPromotion : Promotion
    {
        public char SKUId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public override bool Apply(List<PricingItem> PricingItems)
        {
            bool result = false;
            if (PricingItems.FindAll(j => j.SKUId == SKUId).Sum(j => j.Count) >= Count)
            {
                var n = Count;
                foreach (var pricingItem in PricingItems)
                {
                    if (pricingItem.SKUId != SKUId)
                        continue;

                    var cnt = Math.Min(n, pricingItem.Count);
                    pricingItem.Count -= cnt;
                    n -= cnt;
                    if (n == 0)
                        break;
                }
                PricingItems.RemoveAll(j => j.Count == 0);

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
