namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
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