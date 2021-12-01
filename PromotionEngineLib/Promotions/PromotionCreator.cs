using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineLib.Promotions
{
    public interface IVolumePromotionCreator
    {
        public IPromotion Create(char SKUId, int Count, decimal Price);
    }

    public class VolumePromotionCreator : IVolumePromotionCreator
    {
        public IPromotion Create(char SKUId, int Count, decimal Price)
        {
            return new VolumePromotion() { SKUId = SKUId, Count = Count, Price = Price };
        }
    }

    public interface IBundlePromotionCreator
    {
        public IPromotion Create(char[] BundledSKUIds, decimal Price);
    }

    public class BundlePromotionCreator : IBundlePromotionCreator
    {
        public IPromotion Create(char[] BundledSKUIds, decimal Price)
        {
            return new BundlePromotion() { BundledSKUIds = BundledSKUIds, Price = Price };
        }
    }

    public interface IDiscountPromotionCreator
    {
        public IPromotion Create(char SKUId, decimal Discount);
    }

    public class DiscountPromotionCreator : IDiscountPromotionCreator
    {
        public IPromotion Create(char SKUId, decimal Discount)
        {
            return new DiscountPromotion() { SKUId = SKUId, Discount = Discount };
        }
    }
}
