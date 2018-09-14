namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectCustomerAllStatusModelCriteria : AccountInfoCriteriaBase
    {
        public SelectCustomerAllStatusModelCriteria(int searchAccountId) 
            : base(searchAccountId)
        {
        }

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";
    }
}
