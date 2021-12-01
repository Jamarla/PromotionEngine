using System;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests.TestData
{
    public class TestPriceItem : SUT.Prices.IPriceItem
    {
        public char SKUId { get; set; }
        public decimal Price { get; set; }
    }
}
