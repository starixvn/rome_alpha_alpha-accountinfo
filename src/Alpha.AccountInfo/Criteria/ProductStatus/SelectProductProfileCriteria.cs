namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data.Repository;

    public class SelectProductProfileCriteria : CriteriaBase
    {
        private const byte AlphaSystemId = 2;
        private const int NG2SettingType = 11;
        private const int NotNG2SettingType = 13;

        public SelectProductProfileCriteria(int productId, int settingType)
        {
            ProductId = productId;
            SettingType = settingType;
        }

        public int ProductId { get; }

        public int SettingType { get; }

        public int SystemId { get; } = AlphaSystemId;

        public override string GetSettingKey() => "Acc_CustSetting_ProductProfileSelect";

        public override bool IsValid() => 
            ProductId > 0 && 
            SystemId > 0 && 
            (SettingType == NotNG2SettingType || SettingType == NG2SettingType);
    }
}
