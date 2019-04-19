namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectCustomerSuspendInfoCriteria : AccountInfoCriteriaBase
    {
        public SelectCustomerSuspendInfoCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoSel";
    }
}
