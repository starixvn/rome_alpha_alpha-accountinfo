namespace Alpha.AccountInfo.Tests.Models
{
    using AccountInfo.Models;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class ResultTests
    {
        [Fact]
        public void IsSuccess_ValidCode_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var result = Result.CreateSuccessResult();

            // Act
            var actualResult = result.IsSuccess;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsSuccess_InvalidCode_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var result = Result.CreateErrorResult(500, "Internal Server Error.");

            // Act
            var actualResult = result.IsSuccess;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CreateSuccessResult_Always_ReturnsTrue()
        {
            // Arrange
            var expectedCode = 0;
            var expectedMessage = string.Empty;

            // Act
            var actualResult = Result.CreateSuccessResult(expectedMessage);

            // Assert
            Assert.True(
                actualResult.Code == expectedCode &&
                actualResult.Message == expectedMessage);
        }

        [Fact]
        public void CreateErrorResult_Always_ReturnsTrue()
        {
            // Arrange
            var expectedCode = 500;
            var expectedMessage = "Internal Server Error.";

            // Act
            var actualResult = Result.CreateErrorResult(expectedCode, expectedMessage);

            // Assert
            Assert.True(
                actualResult.Code == expectedCode &&
                actualResult.Message == expectedMessage);
        }

        [Fact]
        public void CreateUnauthorizedResult_Always_ReturnsTrue()
        {
            // Arrange
            var expectedCode = 403;
            var expectedMessage = "You are not authorized on this function.";

            // Act
            var actualResult = Result.CreateForbiddenResult();

            // Assert
            Assert.True(
                actualResult.Code == expectedCode &&
                actualResult.Message == expectedMessage);
        }
    }
}
