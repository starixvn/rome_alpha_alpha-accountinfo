using Alpha.AccountInfo.Configurations;

namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectNG2StatusCriteria : SelectProductStatusCriteria
    {
        public SelectNG2StatusCriteria(int productId, int custId)
            : base(productId, custId)
        {
        }

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Acc_CustSetting_DisableGameStatusSelect";
    }
}
