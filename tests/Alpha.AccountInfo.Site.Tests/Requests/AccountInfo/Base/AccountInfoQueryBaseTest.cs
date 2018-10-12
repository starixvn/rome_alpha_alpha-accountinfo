namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoQueryBaseTest
    {
        private readonly IUser viewer;
        private readonly AccountInfoQueryBase messages;

        public AccountInfoQueryBaseTest()
        {
            viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            messages = new AccountInfoQueryBase(viewer)
            {
                SearchAccountId = 1
            };
        }

        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            Assert.True(messages.SearchAccountId == 1);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void IsValid_Always_ReturnsCorrectValue(int searchAccountId, bool expectedValue)
        {
            var messages = new AccountInfoQueryBase(viewer)
            {
                SearchAccountId = searchAccountId
            };

            Assert.Equal(messages.IsValid(), expectedValue);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        [InlineData(2, true)]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void IsValidSearchAccountRoleId_Always_ReturnsCorrectValue(byte searchAccountRoleId, bool expectedValue)
        {
            Assert.Equal(messages.IsValidSearchAccountRoleId(searchAccountRoleId), expectedValue);
        }

        [Theory]
        [InlineData("test", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void IsValidName_Always_ReturnsCorrectValue(string name, bool expectedValue)
        {
            Assert.Equal(messages.IsValidName(name), expectedValue);
        }
    }
}