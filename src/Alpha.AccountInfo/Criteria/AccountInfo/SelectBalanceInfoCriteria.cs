namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectBalanceInfoCriteria : AccountInfoCriteria
    {
        public SelectBalanceInfoCriteria(int custId) : base(custId)
        {
        }

        public override string GetSettingKey() => "Age_CustomerBalanceSel";
    }
}
