namespace Alpha.AccountInfo.Criteria
{
    using Alpha.AccountInfo.Configurations;
    using Fanex.Data.Repository;

    public class SelectCustomerInformationCriteria : CriteriaBase
    {
        public SelectCustomerInformationCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Age_CustomerSelUserInfo";

        public override bool IsValid() => CustId > 0;
    }
}
