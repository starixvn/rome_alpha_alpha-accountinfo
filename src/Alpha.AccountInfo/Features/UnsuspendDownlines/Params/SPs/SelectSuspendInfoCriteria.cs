namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Params.SPs.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs;

    public class SelectSuspendInfoCriteria : AccountInfoCriteriaBase
    {
        public SelectSuspendInfoCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoSel";
    }
}
