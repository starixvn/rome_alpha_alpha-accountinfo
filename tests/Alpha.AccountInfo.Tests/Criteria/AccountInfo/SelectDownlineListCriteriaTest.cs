namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectDownlineListCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectDownlineListCriteria(1);
            var expectedSettingKey = "Acc_CustomerSelList";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}