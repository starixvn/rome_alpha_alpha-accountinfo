namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
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
            var messages = new CustomerAllStatusQuery(viewer)
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
                messages.GetDisabledProduct(Arg.Any<int>()) == true &&
                messages.GetHideProduct(Arg.Any<int>()) == true &&
                messages.GetSyncedProduct(Arg.Any<int>()) == true &&
                messages.IsHideKeno == true &&
                messages.IsCasinoSync == true &&
                messages.IsHideColossusBets == true &&
                messages.DisableColossusBets == true &&
                messages.IsDisabledESports == true &&
                messages.IsNumberGameDisabled == true);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var messages = new CustomerAllStatusQuery(viewer)
            {
                SearchAccountId = searchAccountId,
                SearchAccountName = "SearchAccountName",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "UplineName",
                UplineRoleId = 4
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }
    }
}
