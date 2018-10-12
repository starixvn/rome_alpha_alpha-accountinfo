namespace Alpha.AccountInfo.Tests.Services
{
    using Alpha.AccountInfo.Features.Shared.Services;
    using Fanex.RemotingRef;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "BaseFunction")]
    public class CacheRefServiceImplTests
    {
        private readonly IRemotingRefService remotingRefService;
        private readonly CacheRefService cacheRefService;

        public CacheRefServiceImplTests()
        {
            remotingRefService = Substitute.For<IRemotingRefService>();
            cacheRefService = new CacheRefServiceImpl(remotingRefService);
        }

        [Fact]
        public void GetBetTypeName_ValidBetTypeId_ReturnsCorrectBetTypeName()
        {
            // Arrange
            var expectedResult = "VIP Baccarat";

            remotingRefService
                .GetBetTypeName("2402", "en-US")
                .Returns(expectedResult);

            // Act
            var actualResult = cacheRefService.GetBetTypeName(2402);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetProductName_ValidProductId_ReturnsCorrectProductName()
        {
            // Arrange
            var expectedResult = "Bullfighting";

            remotingRefService
                .GetResourceDataByRefIdAndResourceId("Product", "30", "en-US", string.Empty)
                .Returns(expectedResult);

            // Act
            var actualResult = cacheRefService.GetProductName(30);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}