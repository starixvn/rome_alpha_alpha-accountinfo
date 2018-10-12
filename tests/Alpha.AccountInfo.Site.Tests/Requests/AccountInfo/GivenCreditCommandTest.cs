namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class GivenCreditCommandTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new GivenCreditCommand(viewer)
            {
                SearchAccountId = 1,
                CreditAmount = 1000,
                SearchAccountRoleId = 1
            };

            Assert.True(
                messages.SearchAccountId == 1 &&
                messages.SearchAccountRoleId == 1 &&
                messages.CreditAmount == 1000 &&
                messages.ActorId == 1 &&
                messages.SubAccountId == 0 &&
                messages.IP == "::1" &&
                messages.Language == "En");
        }
    }
}