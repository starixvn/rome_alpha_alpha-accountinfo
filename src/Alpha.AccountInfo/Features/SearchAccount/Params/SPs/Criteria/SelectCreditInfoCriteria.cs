namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data.Repository;

    public class SelectCreditInfoCriteria : CriteriaBase
    {
        public SelectCreditInfoCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_Customer_CreditInfo_Sel";

        public override bool IsValid() => CustId > 0;
    }
}