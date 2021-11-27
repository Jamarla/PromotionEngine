using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests
{
    public class TotalPrice_Should
    {
        public static Dictionary<char, decimal> TestPriceList =>
            new Dictionary<char, decimal>
            {
                { 'A', 50.0M },
                { 'B', 30.0M },
                { 'C', 20.0M },
                { 'D', 15.0M }
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
                }
            };

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]  //Scenario A
                {
                    new List<SUT.CartItem> 
                    { 
                        new SUT.CartItem() { SKUId = 'A', Count = 1 },
                        new SUT.CartItem() { SKUId = 'B', Count = 1 },
                        new SUT.CartItem() { SKUId = 'C', Count = 1 },
                    },
                    TestPriceList,
                    TestPromotionList,
                    100.0M
                },
                new object[]  //Scenario B
                {
                    new List<SUT.CartItem>
                    {
                        new SUT.CartItem() { SKUId = 'A', Count = 5 },
                        new SUT.CartItem() { SKUId = 'B', Count = 5 },
                        new SUT.CartItem() { SKUId = 'C', Count = 1 },
                    },
                    TestPriceList,
                    TestPromotionList,
                    370.0M
                },
                new object[]  //Scenario C
                {
                    new List<SUT.CartItem>
                    {
                        new SUT.CartItem() { SKUId = 'A', Count = 3 },
                        new SUT.CartItem() { SKUId = 'B', Count = 5 },
                        new SUT.CartItem() { SKUId = 'C', Count = 1 },
                        new SUT.CartItem() { SKUId = 'D', Count = 1 }
                    },
                    TestPriceList,
                    TestPromotionList,
                    280.0M
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateCorrectPrice(
            List<SUT.CartItem> CartItems, 
            Dictionary<char, decimal> PriceList, 
            List<SUT.Promotions.Promotion> PromotionList, 
            decimal ExpectedPrice)
        {
            // Arrange
            var promotionEngine = new SUT.PromotionEngine();

            // Act
            decimal calculatedTotalPrice = promotionEngine.TotalPrice(CartItems, PriceList, PromotionList);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }
    }
}
