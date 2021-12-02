using System;
using System.Collections.Generic;
using SUT = PromotionEngineLib;

namespace PromotionEngineXUnit.Tests.TestData
{
    class TestCartQuery : SUT.Carts.ICartQuery
    {
        private Dictionary<int, IEnumerable<SUT.Carts.ICartItem>> _items;

        public TestCartQuery()
        {
            _items = new Dictionary<int, IEnumerable<SUT.Carts.ICartItem>>()
            {
                {
                    1, new List<TestCartItem>
                    {
                        new TestCartItem() { SKUId = 'A', Count = 1 },
                        new TestCartItem() { SKUId = 'B', Count = 1 },
                        new TestCartItem() { SKUId = 'C', Count = 1 },
                    }
                },
                {
                    2, new List<TestCartItem>
                    {
                        new TestCartItem() { SKUId = 'A', Count = 5 },
                        new TestCartItem() { SKUId = 'B', Count = 5 },
                        new TestCartItem() { SKUId = 'C', Count = 1 },
                    }
                },
                {
                    3, new List<TestCartItem>
                    {
                        new TestCartItem() { SKUId = 'A', Count = 3 },
                        new TestCartItem() { SKUId = 'B', Count = 5 },
                        new TestCartItem() { SKUId = 'C', Count = 1 },
                        new TestCartItem() { SKUId = 'D', Count = 1 }
                    }
                },
                {
                    4, new List<TestCartItem>
                    {
                        new TestCartItem() { SKUId = 'E', Count = 5 }
                    }
                },
                {
                    5, new List<TestCartItem>
                    {
                        new TestCartItem() { SKUId = 'X', Count = 2 }  //Non existing SKUId for exception test
                    }
                }            
            };
        }

        public IEnumerable<SUT.Carts.ICartItem> GetItemsByCartId(int CartId)
        {
            return _items[CartId];
        }
    }
}
