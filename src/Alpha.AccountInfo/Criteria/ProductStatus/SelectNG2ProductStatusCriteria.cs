namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class SelectNG2ProductStatusCriteria : SelectProductStatusCriteria
    {
        public SelectNG2ProductStatusCriteria(int productId, int custId)
            : base(productId, custId)
        {
        }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DisableGameStatusSelect";
        }
    }
}
