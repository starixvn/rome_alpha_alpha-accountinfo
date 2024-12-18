using Fanex.Data;

namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectDownlineListByStatusCriteria : AccountInfoCriteriaBase
    {
        public SelectDownlineListByStatusCriteria(
            int searchAccountId,
            int pageIndex,
            byte roleId,
            byte statusId,
            int totalPages,
            bool isDirectMember) : base(searchAccountId)
        {
            PageIndex = pageIndex;
            RoleId = roleId;
            StatusId = statusId;
            TotalPages = totalPages;
            IsDirectMember = isDirectMember;
        }

        public int PageIndex { get; }

        public byte RoleId { get; }

        [SpParam(SqlParamName = "Status")]
        public byte StatusId { get; }

        public int TotalPages { get; }

        public bool IsDirectMember { get; }

        public override string GetSettingKey() => "Age_CustomerSelListByStatus";
    }
}