using System;
using System.Collections.Generic;

namespace PromotionEngineLib.Carts
{
    public interface ICartQuery
    {
        IEnumerable<ICartItem> GetItemsByCartId(int CartId);
    }
}
