namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectCustomerAllStatusModelCriteria : AccountInfoCriteriaBase
    {
        public SelectCustomerAllStatusModelCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";
    }
}
