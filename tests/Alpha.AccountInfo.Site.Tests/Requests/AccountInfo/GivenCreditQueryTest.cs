namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.SearchAccount;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class GivenCreditQueryTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new GivenCreditQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "test",
                SearchAccountRoleId = 2,
                CurrentGivenCredit = 50
            };

            Assert.True(
                messages.SearchAccountId == 1 &&
                messages.SearchAccountRoleId == 2 &&
                messages.SearchAccountName == "test" &&
                messages.CurrentGivenCredit == 50 &&
                messages.Language == "En");
        }
    }
}