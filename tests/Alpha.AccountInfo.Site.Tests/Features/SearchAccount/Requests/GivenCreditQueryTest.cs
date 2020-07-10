namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
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
                SearchAccountRoleId = 2
            };

            Assert.True(
                messages.SearchAccountId == 1 &&
                messages.SearchAccountRoleId == 2 &&
                messages.SearchAccountName == "test" &&
                messages.Language == "En");
        }
    }
}