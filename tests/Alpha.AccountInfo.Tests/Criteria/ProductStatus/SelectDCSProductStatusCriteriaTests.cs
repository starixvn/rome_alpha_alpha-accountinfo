namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class SelectDCSProductStatusCriteriaTests
    {
        private const int ProductIdTest = 22;
        private const int CustIdTest = 1176083;
        private const byte RoleIdTest = 3;

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectDCSProductStatusCriteria(ProductIdTest, CustIdTest, RoleIdTest);
            var expectedSettingKey = "Acc_CustSetting_StatusSettingSelect";

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
            var criteria = new SelectDCSProductStatusCriteria(ProductIdTest, CustIdTest, RoleIdTest);

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
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, RoleIdTest);

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
            var criteria = new SelectDCSProductStatusCriteria(ProductIdTest, custId, RoleIdTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsValid_InvalidRoleId_ReturnsFalse()
        {
            // Arrange
            var criteria = new SelectDCSProductStatusCriteria(ProductIdTest, CustIdTest, 0);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.False(actualResult);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(1, true)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        public void IsValid_RoleId_ReturnsExpectedResult(byte roleId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectDCSProductStatusCriteria(ProductIdTest, CustIdTest, roleId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
