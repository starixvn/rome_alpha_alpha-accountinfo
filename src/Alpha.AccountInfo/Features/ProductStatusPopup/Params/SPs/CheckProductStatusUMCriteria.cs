namespace Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs
{
    using Fanex.Data.Repository;

    public class CheckProductStatusUMCriteria : CriteriaBase
    {
        public CheckProductStatusUMCriteria(int productId, int custId)
            : this(productId, custId.ToString())
        {
        }

        protected CheckProductStatusUMCriteria(int productId, string custIds)
        {
            ProductId = productId;
            CustIds = custIds;
        }

        public int ProductId { get; }

        public string CustIds { get; }

        public override string GetSettingKey()
            => "Acc_CheckUM_MultipleCustProductStatus";

        public override bool IsValid()
            => ProductId > 0
            && !string.IsNullOrWhiteSpace(CustIds);
    }
}
