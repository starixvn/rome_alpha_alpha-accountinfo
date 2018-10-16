namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup
{
    using System.Linq;
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusCriteriaFactoryImplTests
    {
        private const int CustIdTest = 1176083;
        private const int SubAccIdTest = 536751;
        private const int UserIdTest = 1176070;
        private const string UserNameTest = "User Test";
        private const string IPTest = "127.0.0.1";
        private const string LanguageTest = "E";
        private const bool DisableTest = false;
        private const int UplineIdTest = 1176070;
        private readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
        private readonly UserLevel UplineLevelTest = Enumeration.FromValue<UserLevel>(4);

        private ProductStatusCriteriaFactoryImpl productStatusCriteriaFactoryImpl;

        public ProductStatusCriteriaFactoryImplTests()
        {
            productStatusCriteriaFactoryImpl = new ProductStatusCriteriaFactoryImpl();
        }

        [Theory]
        [InlineData(ProductConstants.RNGCasino)]
        [InlineData(ProductConstants.Keno)]
        [InlineData(ProductConstants.ColossusBets)]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectProductStatusCriteria(productId, CustIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectNG2ProductStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new SelectNG2StatusCriteria(ProductConstants.NG2, CustIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(ProductConstants.NG2, CustIdTest, CustRoleTest.Value);

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
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleTest.Value);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value);

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
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleTest.Value);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateCriteria_Casino_ReturnsUpdateCasinoStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new UpdateCasinoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, DisableTest);
            var productStatus = new[] { new BetTypeStatusParam(208, 2101, false) };
            var param = new ProductStatusCommandParam(
                ProductConstants.RNGCasino,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                UplineLevelTest,
                UserIdTest,
                UserNameTest,
                SubAccIdTest,
                LanguageTest,
                IPTest,
                productStatus);

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
            var productStatus = new[] { new BetTypeStatusParam(204, 18000, false) };
            var param = new ProductStatusCommandParam(
                ProductConstants.ColossusBets,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                UplineLevelTest,
                UserIdTest,
                UserNameTest,
                SubAccIdTest,
                LanguageTest,
                IPTest,
                productStatus);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateUpdateCriteria_NG2_ReturnsUpdateNG2StatusCriteria()
        {
            // Arrange
            var productStatus = new[] { new BetTypeStatusParam(213, 2501, false) };
            var param = new ProductStatusCommandParam(
                ProductConstants.NG2,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                UplineLevelTest,
                UserIdTest,
                UserNameTest,
                SubAccIdTest,
                LanguageTest,
                IPTest,
                productStatus);
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
            var productStatus = new[] { new BetTypeStatusParam(0, 0, false) };
            var param = new ProductStatusCommandParam(
                productId,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                UplineLevelTest,
                UserIdTest,
                UserNameTest,
                SubAccIdTest,
                LanguageTest,
                IPTest,
                productStatus);
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
                new BetTypeStatusParam(202, 1501, false),
                new BetTypeStatusParam(202, 1503, true)
            };
            var param = new ProductStatusCommandParam(
                ProductConstants.NG2,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                UplineLevelTest,
                UserIdTest,
                UserNameTest,
                SubAccIdTest,
                LanguageTest,
                IPTest,
                productStatus);
            var expectedCriteria = new UpdateKenoStatusCriteria(CustIdTest, SubAccIdTest, UserIdTest, IPTest, LanguageTest, 1501, false, UplineIdTest);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateUpdateKenoStatusCriteria(param);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.First().GetSettingKey());
        }
    }
}