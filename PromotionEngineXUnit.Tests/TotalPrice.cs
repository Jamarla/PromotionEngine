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
        private readonly SUT.Promotions.IVolumePromotionCreator _volumePromotionCreator;
        private readonly SUT.Promotions.IBundlePromotionCreator _bundlePromotionCreator;
        private readonly SUT.Promotions.IDiscountPromotionCreator _discountPromotionCreator;

        public TotalPrice_Should(
            SUT.IPromotionEngine promotionEngine,
            SUT.Promotions.IVolumePromotionCreator volumePromotionCreator,
            SUT.Promotions.IBundlePromotionCreator bundlePromotionCreator,
            SUT.Promotions.IDiscountPromotionCreator discountPromotionCreator
        )
        {
            _promotionEngine = promotionEngine;
            _volumePromotionCreator = volumePromotionCreator;
            _bundlePromotionCreator = bundlePromotionCreator;
            _discountPromotionCreator = discountPromotionCreator;
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[]  //Scenario A: 1 * A, 1 * B, 1 * C
                {
                    1,  //CartId
                    100.0M  //Expected price 
                },
                new object[]  //Scenario B: 5 * A, 5 * B, 1 * C
                {
                    2,  //CartId
                    370.0M  //Expected price 
                },
                new object[]  //Scenario C: 3 * A, 5 * B, 1 * C, 1 * D
                {
                    3,  //CartId
                    280.0M  //Expected price 
                },
                new object[]  //Scenario D: 5 * E
                {
                    4,  //CartId
                    158.574M  //Expected price 
                }
            };

        [Theory]
        [MemberData(nameof(TestData))]
        public void CalculateCorrectPrice(
            int CartId,
            decimal ExpectedPrice)
        {
            // Arrange
            var promotions = new List<SUT.Promotions.IPromotion>()
            {
                _volumePromotionCreator.Create(SKUId: 'A', Count: 3, Price: 130),
                _volumePromotionCreator.Create(SKUId: 'B', Count: 2, Price: 45),
                _bundlePromotionCreator.Create(BundledSKUIds: new char[] { 'C', 'D' }, Price: 30),
                _volumePromotionCreator.Create(SKUId: 'E', Count: 2, Price: 62.94M),
                _discountPromotionCreator.Create(SKUId: 'E', Discount: 40.0M)
            };

            // Act
            decimal calculatedTotalPrice = _promotionEngine.TotalPrice(CartId, promotions);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public async Task CalculateCorrectPriceAsynchronous(
            int CartId, 
            decimal ExpectedPrice)
        {
            // Arrange
            var promotions = new List<SUT.Promotions.IPromotion>()
            {
                _volumePromotionCreator.Create(SKUId: 'A', Count: 3, Price: 130),
                _volumePromotionCreator.Create(SKUId: 'B', Count: 2, Price: 45),
                _bundlePromotionCreator.Create(BundledSKUIds: new char[] { 'C', 'D' }, Price: 30),
                _volumePromotionCreator.Create(SKUId: 'E', Count: 2, Price: 62.94M),
                _discountPromotionCreator.Create(SKUId: 'E', Discount: 40.0M)
            };
            
            // Act
            decimal calculatedTotalPrice = await _promotionEngine.TotalPriceAsync(CartId, promotions);

            // Assert
            Assert.Equal(expected: ExpectedPrice, actual: calculatedTotalPrice);
        }
    }
}
