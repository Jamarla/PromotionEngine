using System;
using System.Collections.Generic;

namespace PromotionEngineLib.Prices
{
    public interface IPriceQuery
    {
        IPriceItem GetBySKUId(char SKUId);
    }
}
