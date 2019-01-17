namespace Alpha.AccountInfo.Tests.Services
{
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Models.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Services", "ProductProfileServiceImpl")]
    public class ProductProfileServiceTests
    {
        private readonly IDynamicRepository productProfileDataAccess;
        private readonly ProductProfileService productProfileService;

        public ProductProfileServiceTests()
        {
            productProfileDataAccess = Substitute.For<IDynamicRepository>();
            productProfileService = new ProductProfileService(productProfileDataAccess);
        }

        [Theory]
        [InlineData(6, 1)]
        [InlineData(8, 3)]
        [InlineData(3, 1)]
        [InlineData(12, 1)]
        [InlineData(15, 1)]
        public void GetSettingLevel_NonDCS_ReturnValueFromHardcode(int productId, int exceptedValue)
        {
            // Arrange

            // Act
            var act = productProfileService.GetSettingLevel(productId);

            // Assert
            Assert.Equal(exceptedValue, act);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(16)]
        [InlineData(21)]
        [InlineData(22)]
        [InlineData(23)]
        [InlineData(24)]
        [InlineData(26)]
        [InlineData(28)]
        [InlineData(33)]
        public void GetSettingLevel_DCSProduct_CallRepository(int productId)
        {
            //// Arrange
            productProfileDataAccess.Get<ProductProfile>(Arg.Any<SelectProductProfileCriteria>()).Returns(new ProductProfile());

            //// Act
            var act = productProfileService.GetSettingLevel(productId);

            //// Assert
            productProfileDataAccess.Received().Get<ProductProfile>(Arg.Any<SelectProductProfileCriteria>());
        }
    }
}
