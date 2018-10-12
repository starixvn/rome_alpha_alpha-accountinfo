namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectOnlineListCriteriaCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectOnlineListCriteria(1);
            var expectedSettingKey = "Acc_CustomerSelOnline";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}