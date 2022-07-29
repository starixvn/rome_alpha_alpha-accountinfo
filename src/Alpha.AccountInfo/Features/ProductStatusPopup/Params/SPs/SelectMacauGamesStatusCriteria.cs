using Alpha.AccountInfo.Configurations;

namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectMacauGamesStatusCriteria : SelectDCSProductStatusCriteria
    {
        private const int MacauGamesStatusSettingTypeId = 21;

        public SelectMacauGamesStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId, roleId)
        {
        }

        public new short SettingType => MacauGamesStatusSettingTypeId;
    }
}
