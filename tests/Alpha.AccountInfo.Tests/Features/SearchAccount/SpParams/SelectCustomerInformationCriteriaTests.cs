namespace Alpha.AccountInfo.Tests.Criteria
{
    using Alpha.AccountInfo.Criteria;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class SelectCustomerInformationCriteriaTests
    {
        private const int CustIdTest = 1176083;

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new SelectCustomerInformationCriteria(CustIdTest);
            var expectedSettingKey = "Age_CustomerSelUserInfo";

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
            var criteria = new SelectCustomerInformationCriteria(CustIdTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(int.MinValue, false)]
        [InlineData(0, false)]
        [InlineData(CustIdTest, true)]
        [InlineData(int.MaxValue, true)]
        public void IsValid_CustId_ReturnsExpectedResult(int custId, bool expectedResult)
        {
            // Arrange
            var criteria = new SelectCustomerInformationCriteria(custId);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
