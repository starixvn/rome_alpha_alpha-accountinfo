namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data.Repository;

    public class SelectProductStatusCriteria : CriteriaBase
    {
        public SelectProductStatusCriteria(int productId, int custId)
        {
            ProductId = productId;
            CustId = custId;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerSelStatusByProductId";
        }

        public override bool IsValid()
        {
            return ProductId > 0 && CustId > 0;
        }
    }
}
