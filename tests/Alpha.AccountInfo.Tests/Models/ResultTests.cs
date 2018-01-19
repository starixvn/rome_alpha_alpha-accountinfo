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
            var actualResult = result.IsSuccess();

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
            var actualResult = result.IsSuccess();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CreateSuccessResult_Always_ReturnsTrue()
        {
            // Arrange
            var code = 0;
            var expectedResult = Result.CreateSuccessResult();

            // Act
            var actualResult = code;

            // Assert
            Assert.Equal(expectedResult.Code, actualResult);
        }

        [Fact]
        public void CreateErrorResult_Always_ReturnsTrue()
        {
            // Arrange
            var code = 500;
            var expectedResult = Result.CreateErrorResult(code, "Internal Server Error.");

            // Act
            var actualResult = code;

            // Assert
            Assert.Equal(expectedResult.Code, actualResult);
        }

        [Fact]
        public void CreateUnauthorizedResult_Always_ReturnsTrue()
        {
            // Arrange
            var code = 403;
            var expectedResult = Result.CreateForbiddenResult();

            // Act
            var actualResult = code;

            // Assert
            Assert.Equal(expectedResult.Code, actualResult);
        }
    }
}
