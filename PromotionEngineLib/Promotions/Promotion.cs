using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib.Promotions
{
    public abstract class Promotion
    {
        public abstract bool Apply(List<PricingItem> PricingItems);
    }
}
