namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data.Repository;

    public class AccountInfoCriteria : CriteriaBase
    {
        public AccountInfoCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => string.Empty;

        public override bool IsValid() => CustId > 0;
    }
}
