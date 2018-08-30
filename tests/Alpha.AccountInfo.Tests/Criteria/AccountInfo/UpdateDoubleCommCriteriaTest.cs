namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateDoubleCommCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateDoubleCommCriteria(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                doubleComm: true,
                language: "E",
                ip: "192.168.1.1");

            var expectedSettingKey = "Acc_CustomerUpdDoubleCommLosing";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
