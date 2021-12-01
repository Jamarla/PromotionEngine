using System;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests.TestData
{
    public class TestCartItem : SUT.Carts.ICartItem
    {
        public int CartId { get; set; }
        public char SKUId { get; set; }
        public int Count { get; set; }
    }
}