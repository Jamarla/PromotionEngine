using System;

namespace PromotionEngineLib.Prices
{
    public interface IPriceItem
    {
        char SKUId { get; }
        decimal Price { get; }
    }
}
