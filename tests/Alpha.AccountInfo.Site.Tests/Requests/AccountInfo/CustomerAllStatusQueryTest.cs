namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.SearchAccount;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class CustomerAllStatusQueryTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var messages = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "SearchAccountName",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "UplineName",
                UplineRoleId = 4
            };

            Assert.True(
                messages.SearchAccountId == 1 &&
                messages.SearchAccountName == "SearchAccountName" &&
                messages.SearchAccountRoleId == 3 &&
                messages.UplineId == 2 &&
                messages.UplineName == "UplineName" &&
                messages.UplineRoleId == 4 &&
                messages.ActorId == 1 &&
                messages.ActorRoleId == 4 &&
                messages.ActorName == "ActorName" &&
                messages.SubAccountId == 0 &&
                messages.CanUpdateAccount == true &&
                messages.GetDisabledProduct(Arg.Any<int>()) == true &&
                messages.GetHideProduct(Arg.Any<int>()) == true &&
                messages.GetSyncedProduct(Arg.Any<int>()) == true &&
                messages.IsHideKeno == true &&
                messages.IsCasinoSync == true &&
                messages.IsCasinoDisabled == true &&
                messages.IsHideColossusBets == true &&
                messages.DisableColossusBets == true &&
                messages.IsDisabledESports == true &&
                messages.IsNumberGameDisabled == true);
        }

        [Theory]
        [InlineData(2, 3, true)]
        [InlineData(2, 2, false)]
        [InlineData(2, 1, false)]
        public void IsValidUpline_Always_ReturnsCorrectValue(
            byte searchAccountRoleId,
            byte uplineRoleId,
            bool expectedValue)
        {
            var messages = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "SearchAccountName",
                SearchAccountRoleId = searchAccountRoleId,
                UplineId = 2,
                UplineName = "UplineName",
                UplineRoleId = uplineRoleId
            };

            Assert.Equal(messages.IsValidUpline, expectedValue);
        }

        [Theory]
        [InlineData("test", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void IsValidName_Always_ReturnsCorrectValue(string searchAccountName, bool expectedValue)
        {
            var messages = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = searchAccountName,
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "UplineName",
                UplineRoleId = 4
            };
            Assert.Equal(messages.IsValidName(searchAccountName), expectedValue);
        }
    }
}