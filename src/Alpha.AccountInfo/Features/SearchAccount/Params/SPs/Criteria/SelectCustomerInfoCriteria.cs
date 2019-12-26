namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SelectCustomerInfoCriteria : CriteriaBase
    {
        public SelectCustomerInfoCriteria(string searchAccountName, int uplineId)
        {
            SearchAccountName = searchAccountName;
            UplineId = uplineId;
        }

        [SpParam(SqlParamName = "Username")]
        public string SearchAccountName { get; }

        public int UplineId { get; }

        public override string GetSettingKey() => "Age_CustomerSelAllInfo";

        public override bool IsValid() => !string.IsNullOrEmpty(SearchAccountName) && UplineId > 0;
    }
}
