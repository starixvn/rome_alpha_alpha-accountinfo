namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup.SpParams
{
    using System;
    using Alpha.AccountInfo.Criteria.ProductStatus;

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