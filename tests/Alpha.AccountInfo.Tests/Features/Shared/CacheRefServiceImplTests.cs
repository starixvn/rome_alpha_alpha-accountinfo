namespace Alpha.AccountInfo.Tests.Features.Shared
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
            var expectedResult = "SportBook";

            remotingRefService
                .GetResourceDataByRefIdAndResourceId(
                Arg.Any<string>(),
               Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>())
                .Returns(expectedResult);

            // Act
            var actualResult = cacheRefService.GetProductName(1);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetSportTypeName_ValidSportTypeId_ReturnsCorrectSportypeName()
        {
            // Arrange
            var expectedResult = "SportBook";

            remotingRefService
                .GetSportName("1", "en-US", null)
                .Returns(expectedResult);

            // Act
            var actualResult = cacheRefService.GetSportTypeName(1);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}