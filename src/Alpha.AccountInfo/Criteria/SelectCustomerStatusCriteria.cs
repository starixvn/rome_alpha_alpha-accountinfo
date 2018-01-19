namespace Alpha.AccountInfo.Criteria
{
    using Fanex.Data.Repository;

    public class SelectCustomerStatusCriteria : CriteriaBase
    {
        public SelectCustomerStatusCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_CustomerSelUserInfo";

        public override bool IsValid() => CustId > 0;
    }
}
