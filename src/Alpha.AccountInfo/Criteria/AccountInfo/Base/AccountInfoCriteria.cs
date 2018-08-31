namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class AccountInfoCriteria : CriteriaBase
    {
        public AccountInfoCriteria(int searchAccountId)
        {
            SearchAccountId = searchAccountId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => SearchAccountId > 0;
    }
}
