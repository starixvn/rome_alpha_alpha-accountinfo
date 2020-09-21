namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup.SpParams
{
    using Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class CheckProductStatusUMCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new CheckProductStatusUMCriteria(22, 123456);
            var expectedSettingKey = "Acc_CheckUM_MultipleCustProductStatus";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Theory]
        [InlineData(-22, false)]
        [InlineData(0, false)]
        [InlineData(22, true)]
        public void IsValid_ProductId_ReturnsExpectedResult(int productId, bool expected)
        {
            // Arrange
            var criteria = new CheckProductStatusUMCriteria(productId, 123456);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-123456, true)]
        [InlineData(0, true)]
        [InlineData(123456, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expected)
        {
            // Arrange
            var criteria = new CheckProductStatusUMCriteria(22, custId);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
