namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using AccountInfo.Criteria.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class SelectProductStatusCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(8, 1176070);
            var expectedSettingKey = "Acc_CustomerSelStatusByProductId";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var criteria = new SelectProductStatusCriteria(8, 1176070);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_ProductId_ReturnsExpectedResult(int productId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(productId, 1176070);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(8, custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
