namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using AccountInfo.Criteria.ProductStatus;
    using Constants;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class SelectNG2ProductStatusCriteriaTests
    {
        private const int ProductIdTest = ProductIdConstants.NG2;
        private const int CustIdTest = 1176083;

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectNG2StatusCriteria(ProductIdTest, CustIdTest);
            var expectedSettingKey = "Acc_CustSetting_DisableGameStatusSelect";

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
            var criteria = new SelectNG2StatusCriteria(ProductIdTest, CustIdTest);

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
        public void IsValid_ProductId_ReturnsExpectedResult(int productId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectNG2StatusCriteria(productId, CustIdTest);

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
            var criteria = new SelectNG2StatusCriteria(ProductIdTest, custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
