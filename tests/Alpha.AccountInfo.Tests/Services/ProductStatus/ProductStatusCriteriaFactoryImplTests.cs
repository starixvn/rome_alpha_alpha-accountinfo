namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using AccountInfo.Criteria.ProductStatus;
    using AccountInfo.Services.ProductStatus;
    using Constants;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusCriteriaFactoryImplTests
    {
        private ProductStatusCriteriaFactoryImpl productStatusCriteriaFactoryImpl;

        public ProductStatusCriteriaFactoryImplTests()
        {
            productStatusCriteriaFactoryImpl = new ProductStatusCriteriaFactoryImpl();
        }

        [Theory]
        [InlineData(ProductIdConstants.Racing)]
        [InlineData(ProductIdConstants.RNGCasino)]
        [InlineData(ProductIdConstants.Keno)]
        [InlineData(ProductIdConstants.ColossusBets)]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectProductStatusCriteria(productId, 1176070);
            
            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, 1176070, 3);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Fact]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectNG2ProductStatusCriteria()
        {
            // Arrange
            var expectedCriteria = new SelectNG2ProductStatusCriteria(ProductIdConstants.NG2, 1176070);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(ProductIdConstants.NG2, 1176070, 3);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Theory]
        [InlineData(ProductIdConstants.Allbet)]
        [InlineData(ProductIdConstants.MacauGames)]
        [InlineData(ProductIdConstants.FishingWorld)]
        [InlineData(ProductIdConstants.Lucky3)]
        [InlineData(ProductIdConstants.Lottery)]
        [InlineData(ProductIdConstants.Bullfighting)]
        public void CreateSelectCriteria_ValidProductId_ReturnsSelectDCSProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, 1176070, 3);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, 1176070, 3);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public void CreateSelectCriteria_InvalidProductId_ReturnsSelectDCSProductStatusCriteria(int productId)
        {
            // Arrange
            var expectedCriteria = new SelectDCSProductStatusCriteria(productId, 1176070, 3);

            // Act
            var actualCriteria = productStatusCriteriaFactoryImpl.CreateSelectCriteria(productId, 1176070, 3);

            // Assert
            Assert.Equal(expectedCriteria.GetSettingKey(), actualCriteria.GetSettingKey());
        }
    }
}
