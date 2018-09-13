namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class CustomerStatusCommandTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var messages = new CustomerStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            Assert.True(
                messages.ActorName == "ActorName" &&
                messages.ActorId == 1 &&
                messages.SyncedCasino3rd == true &&
                messages.SearchAccountId == 1 &&
                messages.StatusName == "outright" &&
                messages.StatusValue == true &&
                messages.IP == "::1" &&
                messages.Language == "En");
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var messages = new CustomerStatusCommand(viewer)
            {
                SearchAccountId = searchAccountId,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }

        [Theory]
        [InlineData("outright", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void IsValid_StatusName_ReturnsCorrectValue(string statusName, bool expectedValue)
        {
            var messages = new CustomerStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = statusName,
                StatusValue = true
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }

    }
}
