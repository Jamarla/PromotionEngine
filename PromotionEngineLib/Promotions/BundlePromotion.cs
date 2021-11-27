using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib.Promotions
{
    public class BundlePromotion : Promotion
    {
        public char[] BundledSKUIds { get; set; }
        public override bool Apply(List<PricingItem> PricingItems)
        {
            var lookup = BundledSKUIds.ToLookup(j => j, j => j);
            var isApplicable = true;
            foreach (var key in lookup)
            {
                if (PricingItems.FindAll(j => j.SKUId == key.Key).Sum(j => j.Count) < key.Count())
                {
                    isApplicable = false;
                    break;
                }
            }

            if (!isApplicable)
                return false;

            foreach (var key in lookup)
            {
                RemoveNItemsOfSKU(PricingItems, key.Key, key.Count());
            }

            PricingItems.Add(new PricingItem()
            {
                Description = $"Bundle of { string.Join(',', BundledSKUIds) } promotion",
                Count = 1,
                Price = Price,
                SKUId = ' '
            });

            return true;
        }
    }
}
