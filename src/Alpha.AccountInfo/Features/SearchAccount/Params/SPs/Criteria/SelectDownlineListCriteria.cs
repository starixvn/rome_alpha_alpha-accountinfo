namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectDownlineListCriteria : AccountInfoCriteriaBase
    {
        public SelectDownlineListCriteria(int searchAccountId, int pageIndex) : base(searchAccountId)
        {
            PageIndex = pageIndex;
        }

        public int PageSize { get; } = 20;

        public int PageIndex { get; }

        public bool Mode { get; } = true;

        public override string GetSettingKey() => "Acc_CustomerSelList";
    }
}
