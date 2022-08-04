using Alpha.AccountInfo.Configurations;

namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectMacauGamesStatusCriteria : SelectDCSProductStatusCriteria
    {
        private const short MacauGamesStatusSettingTypeId = 21;

        public SelectMacauGamesStatusCriteria(int productId, int custId, byte roleId)
            : base(productId, custId, roleId)
        {
        }

        public override short SettingType => MacauGamesStatusSettingTypeId;
    }
}
