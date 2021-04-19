namespace Alpha.AccountInfo.Features.OpenDownlines.Params
{
    using SearchAccount.Params.SPs;

    public class SelectCloseInfoDetailCriteria : AccountInfoCriteriaBase
    {
        public SelectCloseInfoDetailCriteria(
            int searchAccountId,
            byte downlineRoleId,
            bool isAffected,
            int pageSize,
            int pageIndex)
            : base(searchAccountId)
        {
            IsAffected = isAffected;
            PageSize = pageSize;
            PageIndex = pageIndex;
            DownlineRoleId = downlineRoleId;
        }

        public bool IsAffected { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        public byte DownlineRoleId { get; }

        public int TotalRows { get; set; }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoDetailSel";
    }
}