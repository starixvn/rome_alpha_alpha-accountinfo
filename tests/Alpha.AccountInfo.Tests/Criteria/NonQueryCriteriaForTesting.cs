namespace Alpha.AccountInfo.Tests.Criteria
{
    using System;
    using AccountInfo.Criteria;

    public class NonQueryCriteriaForTesting : NonQueryCriteria
    {
        public NonQueryCriteriaForTesting(
            int subAccId,
            int userId,
            string language,
            string ip) : base(subAccId, userId, language, ip)
        {
        }

        public override string GetSettingKey()
        {
            throw new NotImplementedException();
        }
    }
}
