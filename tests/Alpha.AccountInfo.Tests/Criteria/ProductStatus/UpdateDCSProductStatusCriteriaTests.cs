namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using System.Collections.Specialized;
    using AccountInfo.Configurations;
    using AccountInfo.Criteria.ProductStatus;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class UpdateDCSProductStatusCriteriaTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";
        private static readonly ProductStatusParam[] ProductStatusParamTest = new[]
        {
            new ProductStatusParam(211, 2401, false),
            new ProductStatusParam(211, 2402, false)
        };

        public UpdateDCSProductStatusCriteriaTests()
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
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, ProductStatusParamTest);
            var expectedSettingKey = "Acc_CustSetting_DisableNewProductStatusUpd";

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
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, ProductStatusParamTest);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsValid_InvalidProductStatusParam_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, null);

            // Act
            var actualResult = criteria.IsValid();

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsDisabled_Always_ReturnsCorrectXML()
        {
            // Arrange
            var expectedResult = @"<ROOT><ProductStatus SportType=""211"" BetType=""2401"" IsDisabled=""0""/><ProductStatus SportType=""211"" BetType=""2402"" IsDisabled=""0""/></ROOT>";
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, ProductStatusParamTest);

            // Act
            var actualResult = criteria.IsDisabled;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void IsDisabled_InvalidProductStatusParam_ReturnsCorrectXML()
        {
            // Arrange
            var expectedResult = string.Empty;
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, null);

            // Act
            var actualResult = criteria.IsDisabled;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
