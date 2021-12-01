using PromotionEngineLib.Prices;
using System;
using System.Collections.Generic;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests.TestData
{
    class TestPriceQuery : SUT.Prices.IPriceQuery
    {
        private Dictionary<char, decimal> _prices;

        public TestPriceQuery()
        {
            _prices = new Dictionary<char, decimal>()
            {
                { 'A', 50.0M },
                { 'B', 30.0M },
                { 'C', 20.0M },
                { 'D', 15.0M },
                { 'E', 54.49M }
            };
        }

        public IPriceItem GetBySKUId(char SKUId)
        {
            IPriceItem result;
            if (!_prices.TryGetValue(SKUId, out var price))
                result = null;
            else
                result = new TestPriceItem() { SKUId = SKUId, Price = price };
            return result;
        }
    }
}
