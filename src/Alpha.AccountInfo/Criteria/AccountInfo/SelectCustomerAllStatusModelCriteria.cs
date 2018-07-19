namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class SelectCustomerAllStatusModelCriteria : AccountInfoCriteria
    {
        public SelectCustomerAllStatusModelCriteria(int custId) : base(custId)
        {
        }

        public override string GetSettingKey() => "Age_CustomerSelAllStatus";
    }
}
