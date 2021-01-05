namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Alpha.AccountInfo.Configurations;

    public class SelectCustomerAllStatusModelCriteria : AccountInfoCriteriaBase
    {
        public SelectCustomerAllStatusModelCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public byte SettingStatus => AppSettings.SettingStatus;

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";
    }
}
