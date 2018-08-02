namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectOnlineListCriteria : AccountInfoCriteria
    {
        public SelectOnlineListCriteria(int custId) : base(custId)
        {
        }

        public override string GetSettingKey() => "Acc_CustomerSelOnline";
    }
}
