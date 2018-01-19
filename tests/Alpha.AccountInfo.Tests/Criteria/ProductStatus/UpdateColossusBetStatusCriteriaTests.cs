namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using System.Collections.Specialized;
    using AccountInfo.Configurations;
    using AccountInfo.Criteria.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class UpdateColossusBetStatusCriteriaTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";
        private const string LanguageTest = "E";
        private const bool DisableTest = false;

        public UpdateColossusBetStatusCriteriaTests()
        {
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SystemId"] = "2"
            };
        }

        [Fact]
        public void GetSettingKey_Always_ReturnsValidSettingKey()
        {
            // Arrange
            var criteria = new UpdateColossusBetStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, DisableTest);
            var expectedSettingKey = "Acc_CustomerUpdDisableColossusBet";

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
            var criteria = new UpdateColossusBetStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, DisableTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(LanguageTest, true)]
        public void IsValid_Language_ReturnsExpectedResult(string language, bool expectedResult)
        {
            // Arrange
            var nonQueryCriteria = new UpdateColossusBetStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, language, DisableTest);

            // Act
            var actualResult = nonQueryCriteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
