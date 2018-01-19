namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using System;
    using AccountInfo.Criteria.ProductStatus;

    public class NonQueryProductStatusCriteriaForTesting : NonQueryProductStatusCriteria
    {
        public NonQueryProductStatusCriteriaForTesting(
            int custId,
            int subAccId,
            int userId,
            string ip) : base(custId, subAccId, userId, ip)
        {
        }

        public override string GetSettingKey()
        {
            throw new NotImplementedException();
        }
    }
}
