using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngineLib.Carts;
using PromotionEngineLib.Prices;

namespace PromotionEngineLib
{
    public interface IPromotionEngine
    {
        decimal TotalPrice(int cartId, List<Promotions.IPromotion> promotionList);
    }

    public class PromotionEngine : IPromotionEngine
    {
        private ICartQuery _cartQuery;
        private IPriceQuery _priceQuery;

        public PromotionEngine(ICartQuery cartQuery, IPriceQuery priceQuery)
        {
            _cartQuery = cartQuery;
            _priceQuery = priceQuery;
        }

        public decimal TotalPrice(int cartId, List<Promotions.IPromotion> promotionList)
        {
            var result = 0.0M;

            var CartItems = _cartQuery.GetItemsByCartId(cartId);

            var pricingItems = new List<IPricingItem>();
            foreach (var cartItem in CartItems)
            {
                var price = _priceQuery.GetBySKUId(cartItem.SKUId);
                if (price == null)
                    throw new ArgumentException($"Unknown SKUId { cartItem.SKUId }");

                pricingItems.Add(new PricingItem
                {
                    SKUId = cartItem.SKUId,
                    Count = cartItem.Count,
                    Price = price.Price,
                    Description = ""
                });
            }

            foreach (var promotion in promotionList)
            {
                do
                { } while (promotion.Apply(pricingItems, _priceQuery));
            }

            result = pricingItems.Sum(j => j.Count * j.Price);

            return result;
        }
    }
}
