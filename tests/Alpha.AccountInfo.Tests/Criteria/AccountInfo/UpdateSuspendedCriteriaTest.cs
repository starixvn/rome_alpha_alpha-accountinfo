namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateSuspendedCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateSuspendedCriteria(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                suspended: true,
                language: "E",
                ip: "192.168.1.1");

            var expectedSettingKey = "Acc_CustomerUpdSuspended";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
