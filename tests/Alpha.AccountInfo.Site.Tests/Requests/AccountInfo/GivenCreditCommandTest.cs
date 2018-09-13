namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class GivenCreditCommandTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
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
                messages.ActorRoleId == 4 &&
                messages.SubAccountId == 0 &&
                messages.IP == "::1" &&
                messages.Language == "En");
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_SearchAccountId_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var messages = new GivenCreditCommand(viewer)
            {
                SearchAccountId = searchAccountId,
                CreditAmount = 1000,
                SearchAccountRoleId = 1
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        public void IsValid_SearchAccountRoleId_ReturnsCorrectValue(byte searchAccountRoleId, bool expectedValue)
        {
            var messages = new GivenCreditCommand(viewer)
            {
                SearchAccountId = 1,
                CreditAmount = 1000,
                SearchAccountRoleId = searchAccountRoleId
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }
    }
}
