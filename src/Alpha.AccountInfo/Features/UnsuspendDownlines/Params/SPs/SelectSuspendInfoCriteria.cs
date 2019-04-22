namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectSuspendInfoCriteria : AccountInfoCriteriaBase
    {
        public SelectSuspendInfoCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoSel";
    }
}
