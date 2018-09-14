namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class AccountInfoCriteriaBase : CriteriaBase
    {
        public AccountInfoCriteriaBase(int searchAccountId)
        {
            SearchAccountId = searchAccountId;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => SearchAccountId > 0;
    }
}
