namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateClosedCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateClosedCriteria(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                closed: true,
                language: "E",
                ip: "192.168.1.1");

            var expectedSettingKey = "Acc_CustomerUpdClosed";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
