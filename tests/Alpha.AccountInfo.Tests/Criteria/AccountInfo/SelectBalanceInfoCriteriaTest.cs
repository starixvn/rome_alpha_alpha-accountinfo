namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectBalanceInfoCriteriaTest
    {
        [Theory]
        [InlineData(1, "Age_BalanceMemberSel")]
        [InlineData(2, "Age_BalanceAgentSel")]
        [InlineData(3, "Age_BalanceMasterSel")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(byte roleId, string expectedSettingKey)
        {
            var criteria = new SelectBalanceInfoCriteria(1, roleId);

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}