namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class CustomerStatusCommandTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var messages = new CustomerNonProductStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            Assert.True(
                messages.ActorName == "ActorName" &&
                messages.ActorId == 1 &&
                messages.SyncedCasino3rd &&
                messages.SearchAccountId == 1 &&
                messages.StatusName == "outright" &&
                messages.StatusValue &&
                messages.IP == "::1" &&
                messages.Language == "E");
        }
    }
}