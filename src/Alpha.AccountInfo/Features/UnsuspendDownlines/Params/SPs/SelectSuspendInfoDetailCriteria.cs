namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data;

    public class SelectSuspendInfoDetailCriteria : AccountInfoCriteriaBase
    {
        public SelectSuspendInfoDetailCriteria(
            int searchAccountId,
            byte roleId,
            bool isAffected,
            int pageSize,
            int pageIndex)
            : base(searchAccountId)
        {
            IsAffected = isAffected;
            PageSize = pageSize;
            PageIndex = pageIndex;
            RoleId = roleId;
        }

        public bool IsAffected { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        [SpParam(SqlParamName = "DownlineRoleid")]
        public byte RoleId { get; }

        public int TotalRows { get; set; }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoDetailSel";
    }
}
