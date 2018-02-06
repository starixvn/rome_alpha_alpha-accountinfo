namespace Alpha.AccountInfo.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using AccountInfo.Helpers;
    using AccountInfo.Models;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ResultHelperTests
    {
        [Fact]
        public void ParseKenoErrorResults_OneErrorCode_ReturnsOneMessage()
        {
            // Arrange
            var errors = new List<Result>
            {
                Result.CreateErrorResult(102, "You are not allowed to disable for downlines.")
            };

            // Act
            var actualResult = ResultHelper.ParseKenoErrorResults(errors);

            // Assert
            Assert.True(
                errors[0].Code == actualResult.Code &&
                errors[0].Message == actualResult.Message);
        }

        [Fact]
        public void ParseKenoErrorResults_ManyErrorCodes_ReturnsOneMessage()
        {
            // Arrange
            var errors = new List<Result>
            {
                Result.CreateErrorResult(102, "You are not allowed to disable for downlines."),
                Result.CreateErrorResult(102, "You are not allowed to disable for downlines.")
            };

            // Act
            var actualResult = ResultHelper.ParseKenoErrorResults(errors);

            // Assert
            Assert.True(
                errors[0].Code == actualResult.Code &&
                errors[0].Message == actualResult.Message);
        }

        [Fact]
        public void ParseKenoErrorResults_ManyErrorCodes_ReturnsManyMessages()
        {
            // Arrange
            var errors = new List<Result>
            {
                Result.CreateErrorResult(102, "You are not allowed to disable for downlines."),
                Result.CreateErrorResult(500, "Internal Server Error.")
            };
            var expectedMessage = string.Join("<br/>", errors.Select(err => err.Message));

            // Act
            var actualResult = ResultHelper.ParseKenoErrorResults(errors);

            // Assert
            Assert.True(
                errors[0].Code == actualResult.Code &&
                expectedMessage == actualResult.Message);
        }

        [Fact]
        public void ParseDCSProductMessage_InvalidXml_ReturnsString()
        {
            // Arrange
            var expectedMessage = "You are not allowed to disable for downlines.";

            // Act
            var actualMessage = ResultHelper.ParseDCSProductMessage(expectedMessage);

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void ParseDCSProductMessage_ValidXml_ReturnsString()
        {
            // Arrange
            var expectedMessage = "You are not allowed to disable for downlines.";
            var xml = $"<Root MsgID=\"60035\" Msg=\"{expectedMessage}\"><Row SportType=\"211\" BetType=\"2401\"/></Root>";

            // Act
            var actualMessage = ResultHelper.ParseDCSProductMessage(xml);

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}
