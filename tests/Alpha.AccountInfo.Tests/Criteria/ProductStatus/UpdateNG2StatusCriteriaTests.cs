namespace Alpha.AccountInfo.Tests.Criteria.ProductStatus
{
    using System.Collections.Specialized;
    using AccountInfo.Configurations;
    using AccountInfo.Criteria.ProductStatus;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class UpdateNG2StatusCriteriaTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";
        private static readonly ProductStatusParam[] ProductStatusParamTest = new[]
        {
            new ProductStatusParam(213, 2501, false)
        };

        public UpdateNG2StatusCriteriaTests()
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
            var criteria = new UpdateNG2StatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, ProductStatusParamTest);
            var expectedSettingKey = "Acc_CustSetting_DisableGameStatusUpd";

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.Equal(expectedSettingKey, actualSettingKey);
        }
    }
}
