namespace Alpha.AccountInfo.Tests.Models
{
    using AccountInfo.Models;
    using Alpha.Products.Core.Shared;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class UserLevelTests
    {
        [Theory]
        [InlineData(4, "Super")]
        [InlineData(3, "Master")]
        [InlineData(2, "Agent")]
        [InlineData(1, "Member")]
        public void GetDisplayName_Always_ReturnsExpectedResult(byte roleId, string expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var actualResult = userLevel.GetDisplayName();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, false)]
        [InlineData(1, true)]
        public void IsMemberLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsMemberLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, true)]
        [InlineData(2, true)]
        [InlineData(1, false)]
        public void IsNotMemberLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsNotMemberLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        [InlineData(1, false)]
        public void IsAgentLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsAgentLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, true)]
        [InlineData(2, false)]
        [InlineData(1, true)]
        public void IsNotAgentLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsNotAgentLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        [InlineData(2, false)]
        [InlineData(1, false)]
        public void IsMasterLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsMasterLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, false)]
        [InlineData(2, true)]
        [InlineData(1, true)]
        public void IsNotMasterLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsNotMasterLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, true)]
        [InlineData(3, false)]
        [InlineData(2, false)]
        [InlineData(1, false)]
        public void IsSuperLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsSuperLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(3, true)]
        [InlineData(2, true)]
        [InlineData(1, true)]
        public void IsNotSuperLevel_Always_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var userLevel = Enumeration.FromValue<UserLevel>(roleId);

            // Act
            var result = userLevel.IsNotSuperLevel();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    
        [Theory]
        [InlineData(1, 3, false)]
        [InlineData(1, 4, false)]
        [InlineData(2, 4, false)]
        [InlineData(1, 2, true)]
        [InlineData(2, 3, true)]
        [InlineData(3, 4, true)]
        public void IsDirectDownlineOf_Always_ReturnsExpectedResult(byte sourceId, byte destinationId, bool expectedResult)
        {
            // Arrange
            var source = Enumeration.FromValue<UserLevel>(sourceId);
            var destination = Enumeration.FromValue<UserLevel>(destinationId);

            // Act
            var result = source.IsDirectDownlineOf(destination);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
