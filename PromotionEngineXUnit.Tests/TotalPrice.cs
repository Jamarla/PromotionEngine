using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SUT = PromotionEngineLib;
using Microsoft.Extensions.DependencyInjection;

namespace PromotionEngineXUnit.Tests
{
    public class TotalPrice_Should
    {
        private readonly SUT.IPromotionEngine _promotionEngine;

        public TotalPrice_Should(SUT.IPromotionEngine promotionEngine)
        {
            _promotionEngine = promotionEngine;
        }

        public static Dictionary<char, decimal> TestPriceList =>
            new Dictionary<char, decimal>
            {
                { 'A', 50.0M },
                { 'B', 30.0M },
                { 'C', 20.0M },
                { 'D', 15.0M },
                { 'E', 54.49M }
            };
        public static List<SUT.Promotions.Promotion> TestPromotionList =>
            new List<SUT.Promotions.Promotion>
            {
                new SUT.Promotions.nItemsOfSKUPromotion
                {
                    SKUId = 'A',
                    Count = 3,
                    Price = 130
                },
                new SUT.Promotions.nItemsOfSKUPromotion
                {
                    SKUId = 'B',
                    Count = 2,
                    Price = 45
                },
                new SUT.Promotions.BundlePromotion
                {
                    BundledSKUIds = new char[] { 'C', 'D' },
                    Price = 30
                },
                new SUT.Promotions.nItemsOfSKUPromotion
                {
                    SKUId = 'E',
                    Count = 2,
                    Price = 62.94M
                },
                new SUT.Promotions.DiscountPromotion
                {
                    SKUId = 'E',
                    Discount = 40.0M
                }
            };

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]  //Scenario A: 1 * A, 1 * B, 1 * C
                {
                    1,  //CartId
                    TestPriceList,
                    TestPromotionList,
                    100.0M  //Expected price 
                },
                new object[]  //Scenario B: 5 * A, 5 * B, 1 * C
                {
                    2,  //CartId
                    TestPriceList,
                    TestPromotionList,
                    370.0M  //Expected price 
                },
                new object[]  //Scenario C: 3 * A, 5 * B, 1 * C, 1 * D
                {
                    3,  //CartId
                    TestPriceList,
                    TestPromotionList,
                    280.0M  //Expected price 
                },
                new object[]  //Scenario D: 5 * E
                {
                    4,  //CartId
                    TestPriceList,
                    TestPromotionList,
                    158.574M  //Expected price 
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateCorrectPrice(
            int CartId, 
            Dictionary<char, decimal> PriceList, 
            List<SUT.Promotions.Promotion> PromotionList, 
            decimal ExpectedPrice)
        {
            // Arrange

            // Act
            decimal calculatedTotalPrice = _promotionEngine.TotalPrice(CartId, PriceList, PromotionList);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }
    }
}
