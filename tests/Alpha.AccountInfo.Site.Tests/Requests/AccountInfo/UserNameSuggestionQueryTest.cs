namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class UserNameSuggestionQueryTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new UserNameSuggestionQuery(viewer)
            {
                SuggestionQuery = "test"
            };

            Assert.True(
                messages.SuggestionQuery == "test" &&
                messages.ActorId == 1 &&
                messages.IsInternal == false);
        }
    }
}