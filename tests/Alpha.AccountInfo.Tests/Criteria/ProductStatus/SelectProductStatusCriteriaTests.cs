namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class SelectProductStatusCriteriaTests
    {
        private const int ProductIdTest = ProductConstants.Keno;
        private const int CustIdTest = 1176083;

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(ProductIdTest, CustIdTest);
            var expectedSettingKey = "Acc_AdminSelStatusByProductId";

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
            var criteria = new SelectProductStatusCriteria(ProductIdTest, CustIdTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(ProductIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_ProductId_ReturnsExpectedResult(int productId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(CustIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectProductStatusCriteria(ProductIdTest, custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
