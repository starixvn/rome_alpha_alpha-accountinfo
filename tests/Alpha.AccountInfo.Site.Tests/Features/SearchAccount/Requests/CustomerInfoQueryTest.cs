namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class CustomerInfoQueryTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var messages = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = "SearchAccountName"
            };

            Assert.True(
                messages.SearchAccountName == "SearchAccountName" &&
                messages.ActorId == 1 &&
                messages.ActorRoleId == 4 &&
                messages.AllDayBalTransfer == false &&
                messages.AllowedTotalBalanceTransfer == true &&
                messages.AllowTransfer == true &&
                messages.ViewReports == true);
        }

        [Theory]
        [InlineData("SearchAccountName", true)]
        [InlineData("ActorName", false)]
        public void IsValid_Always_ReturnsCorrectValue(string userName, bool expectedValue)
        {
            var messages = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = userName
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }
    }
}