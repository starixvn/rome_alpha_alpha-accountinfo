namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectDCSProductStatusFor4Criteria : SelectProductStatusCriteria
    {
        public SelectDCSProductStatusFor4Criteria(int productId, int custId)
            : base(productId, custId)
        {
        }

        public override string GetSettingKey() => "Acc_CustSetting_DisableProductStatusSelect";
    }
}