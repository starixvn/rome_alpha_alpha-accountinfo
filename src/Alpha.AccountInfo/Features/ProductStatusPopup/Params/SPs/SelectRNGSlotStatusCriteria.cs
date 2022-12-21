using Alpha.AccountInfo.Configurations;

namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectRNGSlotStatusCriteria : SelectDCSProductStatusCriteria
    {
        private const short RNGSlotStatusSettingTypeId = 21;

        public SelectRNGSlotStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId, roleId)
        {
        }

        public override short SettingType => RNGSlotStatusSettingTypeId;
    }
}
