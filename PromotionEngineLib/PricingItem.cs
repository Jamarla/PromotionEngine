using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib
{
    internal interface IPricingItem
    {
        string Description { get; set; }
        char SKUId { get; set; }
        int Count { get; set; }
        decimal Price { get; set; }
    }

    internal class PricingItem : IPricingItem
    {
        public string Description { get; set; }
        public char SKUId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
