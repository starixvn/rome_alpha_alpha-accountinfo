namespace Alpha.AccountInfo.Site.Tests.Models
{
    using NSubstitute;
    using Site.Models;
    using Sunplus.Agent.Model.Entities;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class UserTests
    {
        [Fact]
        public void Constructor_Always_GetUserDataFromSession()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.CustId.Returns(1);
            userSession.UserName.Returns("username");
            userSession.RoleId.Returns((byte)2);
            userSession.SubAccId.Returns(3);
            userSession.SubUserName.Returns("subaccname");
            userSession.UserLoginIp.Returns("::1");
            userSession.IsInternal.Returns(false);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.True(
                user.Id == 1 &&
                user.Name == "username" &&
                user.LevelId == 2 &&
                user.SubAccId == 3 &&
                user.SubAccName == "subaccname" &&
                user.IP == "::1" &&
                user.IsInternal == false);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanUpdateAccount_Always_ExpectedValue(bool expectedValue)
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.SubPageAccess.Returns(new PageAccess { UpdateAccount = expectedValue });
            var user = new User(userSession);

            // Act
            var act = user.CanUpdateAccount;

            // Assert
            Assert.Equal(expectedValue, act);
        }
    }
}
