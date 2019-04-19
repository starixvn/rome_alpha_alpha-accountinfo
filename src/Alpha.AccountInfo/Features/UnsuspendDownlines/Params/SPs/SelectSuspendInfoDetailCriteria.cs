namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data;

    public class SelectSuspendInfoDetailCriteria : AccountInfoCriteriaBase
    {
        public SelectSuspendInfoDetailCriteria(
            int searchAccountId,
            bool isAffected,
            int pageSize,
            int pageIndex,
            byte roleId,
            int totalPages)
            : base(searchAccountId)
        {
            IsAffected = isAffected;
            PageSize = pageSize;
            PageIndex = pageIndex;
            RoleId = roleId;
            TotalPages = totalPages;
        }

        public bool IsAffected { get; }

        public int PageSize { get; }

        public int PageIndex { get; }

        [SpParam(SqlParamName = "DownlineRoleid")]
        public byte RoleId { get; }

        public int TotalPages { get; }

        public override string GetSettingKey() => "Acc_Customer_SuspendInfoDetailSel";
    }
}
