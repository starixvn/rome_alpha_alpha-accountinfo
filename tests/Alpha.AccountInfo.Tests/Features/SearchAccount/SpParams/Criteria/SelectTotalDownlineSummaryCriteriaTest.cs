namespace Alpha.AccountInfo.Tests.Features.SearchAccount.SpParams
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectTotalDownlineSummaryCriteriaTest
    {
        [Fact]
        public void SelectTotalDownlineSummaryCriteria_Always_ReturnsCorrectProperties()
        {
            var criteria = new SelectTotalDownlineSummaryCriteria(1, 2);
            var expectedSettingKey = "Acc_CustomerSelTotalDownline";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(2, criteria.RoleId);
            Assert.Equal(1, criteria.SearchAccountId);
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}