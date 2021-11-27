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
                    370.0M
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateCorrectPrice(List<SUT.CartItem> CartItems, Dictionary<char, decimal> PriceList, decimal ExpectedPrice)
        {
            // Arrange
            var promotionEngine = new SUT.PromotionEngine();

            // Act
            decimal calculatedTotalPrice = promotionEngine.TotalPrice(CartItems, PriceList);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }
    }
}
