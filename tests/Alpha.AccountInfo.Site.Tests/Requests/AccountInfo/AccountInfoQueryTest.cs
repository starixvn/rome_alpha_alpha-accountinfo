namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoQueryTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new AccountInfoQuery(viewer);

            Assert.True(
                messages.ActorRoleId == 4 &&
                messages.ActorName == "ActorName" &&
                messages.Language == "En");
        }
    }
}