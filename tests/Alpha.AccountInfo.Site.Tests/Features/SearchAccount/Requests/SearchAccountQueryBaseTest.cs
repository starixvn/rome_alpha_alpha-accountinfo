namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountQueryBaseTest
    {
        private readonly IUser viewer;
        private readonly SearchAccountQueryBase messages;

        public SearchAccountQueryBaseTest()
        {
            viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            messages = new SearchAccountQueryBase(viewer)
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
        [InlineData(4, 3, false)]
        [InlineData(3, 4, true)]
        public void IsValidSearchAccountRoleId_Always_ReturnsCorrectValue(byte searchAccountRoleId, byte viewerRoleId, bool expectedValue)
        {
            var fakeViewer = AccountInfoHelper.ViewerFixture(viewerRoleId);
            var fakeMessages = new SearchAccountQueryBase(fakeViewer)
            {
                SearchAccountId = 1
            };

            Assert.Equal(fakeMessages.IsValidSearchAccountRoleId(searchAccountRoleId), expectedValue);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(1, false)]
        [InlineData(0, false)]
        public void IsValid_InputSearchAccountRoleId_Always_ReturnsCorrectValue(byte searchAccountId, bool expectedValue)
        {
            var fakeMessages = new SearchAccountQueryBase(viewer)
            {
                SearchAccountId = searchAccountId
            };

            Assert.Equal(fakeMessages.IsValid(), expectedValue);
        }


        [Theory]
        [InlineData("test", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void IsValid_InputSearchAccountName_Always_ReturnsCorrectValue(string name, bool expectedValue)
        {
            Assert.Equal(messages.IsValidName(name), expectedValue);
        }
    }
}