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
        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]
                {
                    new char[] { 'A', 'B', 'C' },
                    new Dictionary<char, decimal>()
                    {
                        { 'A', 50.0M },
                        { 'B', 30.0M },
                        { 'C', 20.0M },
                        { 'D', 15.0M }
                    },
                    100.0M
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateCorrectPrice(char[] SKUIds, Dictionary<char, int> PriceList, decimal ExpectedPrice)
        {
            // Arrange
            var promotionEngine = new SUT.PromotionEngine();

            // Act
            decimal calculatedTotalPrice = promotionEngine.TotalPrice(SKUIds, PriceList);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }
    }
}
