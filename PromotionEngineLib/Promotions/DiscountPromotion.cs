using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib.Promotions
{
    public class DiscountPromotion : Promotion, IPromotion
    {
        public char SKUId { get; set; }
        //Discount in percentage. For example 15.0
        public decimal Discount { get; set; }
        bool IPromotion.Apply(List<IPricingItem> PricingItems)
        {
            var item = PricingItems.FindLast(j => j.SKUId == SKUId);
            if (item == null)
                return false;

            var discountedPrice = item.Price * (100.0M - Discount) / 100.0M;

            RemoveNItemsOfSKU(PricingItems, SKUId, 1);

            PricingItems.Add(new PricingItem()
            {
                Description = $"Discount on { SKUId } promotion",
                Count = 1,
                Price = discountedPrice,
                SKUId = ' '
            });

            return true;
        }
    }
}
