namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectDownlineListCriteria : AccountInfoCriteriaBase
    {
        public SelectDownlineListCriteria(int searchAccountId) : base(searchAccountId)
        {
        }

        public bool Mode { get; } = true;

        public override string GetSettingKey() => "Acc_CustomerSelList";
    }
}
