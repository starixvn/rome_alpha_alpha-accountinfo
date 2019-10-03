namespace Alpha.AccountInfo.Site.Tests.Features.Shared
{
    using Alpha.AccountInfo.Site.Features.Shared.Models;
    using NSubstitute;
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
            userSession.UserId.Returns(8);
            userSession.UserLoginIp.Returns("::1");
            userSession.LangKey.Returns("E");
            userSession.Language.Returns("En");
            userSession.IsInternal.Returns(false);
            userSession.IsAllDayBalTransfer.Returns(true);
            userSession.IsAllowedTotalBalanceTransfer.Returns(true);

            userSession.SubPageAccess.Returns(new PageAccess { Transfer = true, ViewReports = true });

            userSession.GetDisabledProduct(Arg.Any<int>()).Returns(true);
            userSession.GetHideProduct(Arg.Any<int>()).Returns(true);
            userSession.GetSyncedProduct(Arg.Any<int>()).Returns(true);
            userSession.IsHideKeno.Returns(true);
            userSession.IsCasinoSync.Returns(true);
            userSession.IsCasinoDisabled.Returns(true);
            userSession.IsDisabledESports.Returns(true);
            userSession.IsNumberGameDisabled.Returns(true);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.True(
                user.Id == 1 &&
                user.Name == "username" &&
                user.LevelId == 2 &&
                user.SubAccId == 3 &&
                user.SubAccName == "subaccname" &&
                user.UserId == 8 &&
                user.IP == "::1" &&
                user.LangKey == "E" &&
                user.Language == "En" &&
                !user.IsInternal &&
                user.AllowedAllDayBalTransfer &&
                user.AllowedTotalBalanceTransfer &&
                user.CanViewReports &&
                user.AllowTransfer &&
                user.GetDisabledProduct(1) &&
                user.GetHideProduct(1) &&
                user.GetSyncedProduct(1) &&
                user.IsHideKeno &&
                user.IsCasinoSync &&
                user.IsCasinoDisabled &&
                user.IsDisabledESports &&
                user.IsNumberGameDisabled);
        }

        [Fact]
        public void CanUpdateAccount_MainAccount_ReturnsTrue()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.IsSubAcc.Returns(false);
            var user = new User(userSession);

            // Act
            var actualResult = user.CanUpdateAccount;

            // Assert
            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CanUpdateAccount_Always_ExpectedValue(bool expectedValue)
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.IsSubAcc.Returns(true);
            userSession.SubPageAccess.Returns(new PageAccess { UpdateAccount = expectedValue });
            var user = new User(userSession);

            // Act
            var act = user.CanUpdateAccount;

            // Assert
            Assert.Equal(expectedValue, act);
        }
    }
}