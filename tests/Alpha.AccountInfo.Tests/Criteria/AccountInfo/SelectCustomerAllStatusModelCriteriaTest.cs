namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectCustomerAllStatusModelCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCustomerAllStatusModelCriteria(1);
            var expectedSettingKey = "Age_CustomerSelAllStatus";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}