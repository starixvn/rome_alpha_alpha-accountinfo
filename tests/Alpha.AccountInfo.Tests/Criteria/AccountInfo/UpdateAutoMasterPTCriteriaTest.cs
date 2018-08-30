namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UpdateAutoMasterPTCriteriaTest
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            var criteria = new UpdateAutoMasterPTCriteria(
                searchAccountId: 1,
                actorId: 2,
                subAccountId: 0,
                disableAutoMasterPT: true,
                language: "E",
                ip: "192.168.1.1");

            var expectedSettingKey = "Acc_CustomerUpdMaster_DisableAutoMPT";

            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
