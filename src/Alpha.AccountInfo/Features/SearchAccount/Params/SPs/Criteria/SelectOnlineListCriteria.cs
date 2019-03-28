namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectOnlineListCriteria : AccountInfoCriteriaBase
    {
        public SelectOnlineListCriteria(int searchAccountId) : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_CustomerSelOnline";
    }
}
