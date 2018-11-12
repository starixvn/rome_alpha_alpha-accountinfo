using Fanex.Data;

namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    public class SelectDownlineListByStatusCriteria : AccountInfoCriteriaBase
    {
        private const int PAGESIZE = 10;

        public SelectDownlineListByStatusCriteria(
            int searchAccountId, 
            int pageIndex, 
            byte roleId,
            byte statusId,
            int totalPages) : base(searchAccountId)
        {
            PageIndex = pageIndex;
            RoleId = roleId;
            StatusId = statusId;
            TotalPages = totalPages;
        }

        public int PageSize { get; } = PAGESIZE;

        public int PageIndex { get; }

        public byte RoleId { get; }

        [SpParam(SqlParamName = "Status")]
        public byte StatusId { get; }

        public int TotalPages { get; }

        public override string GetSettingKey() => "Age_CustomerSelListByStatus";
    }
}
