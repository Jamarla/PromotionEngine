using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib
{
    public class PricingItem
    {
        public string Description { get; set; }
        public char SKUId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
