namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountQueryTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new SearchAccountQuery(viewer);

            Assert.True(
                messages.ActorRoleId == 4 &&
                messages.ActorName == "ActorName" &&
                messages.Language == "En");
        }
    }
}