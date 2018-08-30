namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SelectCustomerInfoCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new SelectCustomerInfoCriteria("UserTest", 1);
            var expectedSettingKey = "Age_CustomerSelAllInfo";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("UserTest", true)]
        public void IsValid_Username_Should_ReturnsValidSettingKey(string username, bool expectedIsValid)
        {
            var criteria = new SelectCustomerInfoCriteria(username, 1);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }


        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void IsValid_UplineId_Should_ReturnsValidSettingKey(int uplineId, bool expectedIsValid)
        {
            var criteria = new SelectCustomerInfoCriteria("UserTest", uplineId);

            Assert.Equal(expectedIsValid, criteria.IsValid());
        }
    }
}
