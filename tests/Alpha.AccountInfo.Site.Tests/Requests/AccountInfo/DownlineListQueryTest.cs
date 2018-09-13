namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class DownlineListQueryTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var messages = new DownlineListQuery(viewer)
            {
                SearchAccountId = 1
            };

            Assert.True(messages.SearchAccountId == 1);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_InvalidSearchAccountId_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var messages = new DownlineListQuery(viewer)
            {
                SearchAccountId = searchAccountId
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }
    }
}
