
namespace Alpha.AccountInfo.Tests.Criteria.AccountInfo
{
    using Alpha.AccountInfo.Criteria.AccountInfo;
    using Xunit;
    using static Alpha.AccountInfo.Constants.AccountInfoDataKey;

    [Trait("Category", "AccountInfo")]
    public class UpdateCustomerStatusCriteriaTest
    {
        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_Should_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var criteria = new UpdateCustomerStatusCriteria(searchAccountId, 1462, 0, "E", "192.168.1.1", StatusMapping.Closed);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        public void IsValid_ActorId_Should_ReturnsCorrectValue(int actorId, bool expectedValue)
        {
            var criteria = new UpdateCustomerStatusCriteria(123, actorId, 0, "E", "192.168.1.1", StatusMapping.Closed);

            Assert.Equal(expectedValue, criteria.IsValid());
        }

        [Theory]
        [InlineData(StatusMapping.DisableAutoMasterPT, "Acc_CustomerUpdMaster_DisableAutoMPT")]
        [InlineData(StatusMapping.Closed, "Acc_CustomerUpdClosed")]
        [InlineData(StatusMapping.DoubleComm, "Acc_CustomerUpdDoubleCommLosing")]
        [InlineData(StatusMapping.DisableOutright, "Acc_CustomerUpdDisableOutright")]
        [InlineData(StatusMapping.Suspended, "Acc_CustomerUpdSuspended")]
        public void GetSettingKey_Always_ReturnsValidSettingKey(string statusName, string expectedSettingKey)
        {
            var criteria = new UpdateCustomerStatusCriteria(1, 2, 0, "E", "192.168.1.1", statusName);
            var actualSettingKey = criteria.GetSettingKey();

            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
