namespace Alpha.AccountInfo.Criteria.AccountInfo
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
