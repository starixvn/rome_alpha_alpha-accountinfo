namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using System.Collections.Specialized;
    using System.Linq;
    using AccountInfo.Configurations;
    using AccountInfo.Criteria.ProductStatus;
    using AccountInfo.Services.ProductStatus;
    using Constants;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusCriteriaFactoryImplTests
    {
        private const int CustIdTest = 1176083;
        private const byte CustRoleIdTest = 3;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string IPTest = "127.0.0.1";
        private const string LanguageTest = "E";
        private const bool DisableTest = false;
        private const int UplineIdTest = 1176070;

        private ProductStatusCriteriaFactoryImpl productStatusCriteriaFactoryImpl;

        public ProductStatusCriteriaFactoryImplTests()
        {
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SystemId"] = "2"
            };

            productStatusCriteriaFactoryImpl = new ProductStatusCriteriaFactoryImpl();
        }

        [Theory]
        [InlineData(ProductIdConstants.RNGCasino)]
        [InlineData(ProductIdConstants.Keno)]
        [InlineData(ProductIdConstants.ColossusBets)]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectProductStatusCriteria(productId, CustIdTest);
            
            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleIdTest);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectNG2ProductStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new SelectNG2StatusCriteria(ProductIdConstants.NG2, CustIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(ProductIdConstants.NG2, CustIdTest, CustRoleIdTest);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Theory]
        [InlineData(21)]
        [InlineData(22)]
        [InlineData(24)]
        [InlineData(26)]
        [InlineData(28)]
        [InlineData(30)]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectDCSProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleIdTest);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void CreateSelectCriteria_InvalidProductId_ReturnsSelectDCSProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleIdTest);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateCriteria_Casino_ReturnsUpdateCasinoStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new UpdateCasinoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, DisableTest);
            var productStatus = new[] { new ProductStatusParam(208, 2101, false) };
            var param = new UpdateProductStatusParam(ProductIdConstants.RNGCasino, CustIdTest, UplineIdTest, UserIdTest, SubAccIdTest, LanguageTest, IPTest, productStatus);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateCriteria_ColossusBets_ReturnsUpdateColossusBetStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new UpdateColossusBetStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, DisableTest);
            var productStatus = new[] { new ProductStatusParam(204, 18000, false) };
            var param = new UpdateProductStatusParam(ProductIdConstants.ColossusBets, CustIdTest, UplineIdTest, UserIdTest, SubAccIdTest, LanguageTest, IPTest, productStatus);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateCriteria_NG2_ReturnsUpdateNG2StatusCriteria()
        {
            // Arrange
            var productStatus = new[] { new ProductStatusParam(213, 2501, false) };
            var param = new UpdateProductStatusParam(ProductIdConstants.NG2, CustIdTest, UplineIdTest, UserIdTest, SubAccIdTest, LanguageTest, IPTest, productStatus);
            var expectedCriteria = new UpdateNG2StatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, productStatus);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Theory]
        [InlineData(21)]
        [InlineData(30)]
        public void CreateUpdateCriteria_DCSProduct_ReturnsUpdateDCSProductStatusCriteria(int productId)
        {
            // Arrange
            var productStatus = new[] { new ProductStatusParam(0, 0, false) };
            var param = new UpdateProductStatusParam(productId, CustIdTest, UplineIdTest, UserIdTest, SubAccIdTest, LanguageTest, IPTest, productStatus);
            var expectedCriteria = new UpdateDCSProductStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, productStatus);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateKenoStatusCriteria_Always_ReturnsCorrectCriteria()
        {
            // Arrange
            var productStatus = new[]
            {
                new ProductStatusParam(202, 1501, false),
                new ProductStatusParam(202, 1503, true)
            };
            var param = new UpdateProductStatusParam(ProductIdConstants.NG2, CustIdTest, UplineIdTest, UserIdTest, SubAccIdTest, LanguageTest, IPTest, productStatus);
            var expectedCriteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, 1501, false, UplineIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateKenoStatusCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.First().GetSettingKey());
        }
    }
}
