namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectDownlineListCriteria : AccountInfoCriteria
    {
        public SelectDownlineListCriteria(int custId) : base(custId)
        {
        }

        public bool Mode { get; } = true;

        public override string GetSettingKey() => "Acc_CustomerSelList";
    }
}
