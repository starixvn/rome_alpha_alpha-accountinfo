namespace Alpha.AccountInfo.Tests.Helpers
{
    using AccountInfo.Helpers;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ResultHelperTests
    {
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
