using System;

namespace PromotionEngineLib.Carts
{
    public interface ICartItem
    {
        int CartId { get; }
        char SKUId { get; }
        int Count { get; }
    }
}
