namespace Alpha.AccountInfo.Site.Tests.Requests.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class AccountInfoQueryTest
    {
        [Fact]
        public void Constructor_Always_ReturnCorrectRequest()
        {
            var viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
            var messages = new AccountInfoQuery(viewer);

            Assert.True(
                messages.ActorRoleId == 4 &&
                messages.ActorName == "ActorName" &&
                messages.Language == "En");
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        public void IsValid_Always_ReturnCorrectValue(byte actorRoleId, bool expectedValue)
        {
            var viewer = Substitute.For<IUser>();
            viewer.LevelId.Returns(actorRoleId);
            viewer.Name.Returns("Actor Name");
            viewer.Language.Returns("En");

            var messages = new AccountInfoQuery(viewer);
            
            Assert.Equal(messages.IsValid(), expectedValue);
        }
    }
}
