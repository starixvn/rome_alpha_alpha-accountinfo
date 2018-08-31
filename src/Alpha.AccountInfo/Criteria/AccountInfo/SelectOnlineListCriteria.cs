namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectOnlineListCriteria : AccountInfoCriteria
    {
        public SelectOnlineListCriteria(int searchAccountId) : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Acc_CustomerSelOnline";
    }
}
