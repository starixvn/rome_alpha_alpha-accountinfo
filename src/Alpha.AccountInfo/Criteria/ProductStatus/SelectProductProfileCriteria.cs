namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data.Repository;

    public class SelectProductProfileCriteria : CriteriaBase
    {
        public SelectProductProfileCriteria(int productId, int settingType)
        {
            ProductId = productId;
            SettingType = settingType;
        }

        public int ProductId { get; }

        public int SettingType { get; }

        public int SystemId { get; } = 2;

        public override string GetSettingKey() => "Acc_CustSetting_ProductProfileSelect";

        public override bool IsValid() => ProductId > 0 && SystemId > 0 && (SettingType == 13 || SettingType == 11);
    }
}
