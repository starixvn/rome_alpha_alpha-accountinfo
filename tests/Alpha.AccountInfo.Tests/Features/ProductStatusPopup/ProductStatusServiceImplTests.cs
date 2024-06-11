namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Models;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.Services;
    using Alpha.AccountInfo.Models.ProductStatus;
    using Alpha.AccountInfo.Services.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using Fanex.Logging;
    using NSubstitute;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusServiceImplTests
    {
        private const int CustIdTest = 1176083;
        private const int UplineIdTest = 8;
        private readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
        private readonly UserLevel UplineLevelTest = Enumeration.FromValue<UserLevel>(4);

        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly ProductStatusServiceImpl productStatusServiceImpl;
        private readonly ILogger logger;

        public ProductStatusServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            criteriaFactory = Substitute.For<ProductStatusCriteriaFactory>();
            authorizationService = Substitute.For<ProductStatusAuthorizationService>();
            logger = Substitute.For<ILogger>();
            productStatusServiceImpl = new ProductStatusServiceImpl(
                dynamicRepository,
                cacheRefService,
                criteriaFactory,
                authorizationService,
                logger);
        }

        [Fact]
        public async Task GetProductStatusAsync_DCSSingleBetType_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = 22;
            var productName = "Macau Games";
            var expectedResults = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, false)
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleTest.Value);
            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(expectedResults);
            cacheRefService
                .GetProductName(productId)
                .Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(new ProductStatusAuthorizationParam(false, productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest, Array.Empty<int>()));
            var actualResult = actualData.First();

            // Assert
            Assert.True(
                expectedResults[0].ProductId == actualResult.ProductId &&
                expectedResults[0].SportType == actualResult.SportType &&
                expectedResults[0].BetType == actualResult.BetType &&
                expectedResults[0].IsDisabled == actualResult.IsDisabled &&
                expectedResults[0].IsDisabledUpline == actualResult.IsDisabledUpline &&
                expectedResults[0].Name == actualResult.Name);
        }

        [Fact]
        public async Task GetProductStatusAsync_DCSMultipleBetTypes_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = 21;
            var sportType = 211;
            var expectedResults = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 2401, false, false),
                new ProductStatusModel(productId, sportType, 2402, false, false)
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, CustRoleTest.Value);
            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(expectedResults);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(new ProductStatusAuthorizationParam(false, productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest, Array.Empty<int>()));
            var actualResults = actualData.ToList();

            // Assert
            Assert.True(
                expectedResults[0].SportType == actualResults[0].SportType &&
                expectedResults[0].BetType == actualResults[0].BetType &&
                expectedResults[0].IsDisabled == actualResults[0].IsDisabled &&
                expectedResults[0].IsDisabledUpline == actualResults[0].IsDisabledUpline &&
                expectedResults[0].Name == actualResults[0].Name &&
                expectedResults[1].SportType == actualResults[1].SportType &&
                expectedResults[1].BetType == actualResults[1].BetType &&
                expectedResults[1].IsDisabled == actualResults[1].IsDisabled &&
                expectedResults[1].IsDisabledUpline == actualResults[1].IsDisabledUpline &&
                expectedResults[1].Name == actualResults[1].Name);
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsSucessfulResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                false,
                30,
                CustIdTest,
                CustRoleTest,
                0,
                UplineLevelTest,
                0,
                string.Empty,
                0,
                null,
                null,
                Enumerable.Empty<BetTypeStatusParam>(),
                "en-US", Array.Empty<int>());
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", string.Empty);
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);
            dynamicRepository
                .FetchAsync<ProductStatusUM>(Arg.Any<CheckProductStatusUMCriteria>())
                .Returns(Enumerable.Empty<ProductStatusUM>());

            // Act
            var actualResult = (await productStatusServiceImpl.UpdateProductStatusAsync(commandParam)).FirstOrDefault();

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsErrorResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                false,
                30,
                CustIdTest,
                CustRoleTest,
                0,
                UplineLevelTest,
                0,
                string.Empty,
                0,
                null,
                null,
                null,
                "en-US",
                Array.Empty<int>());
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", string.Empty)
            {
                ErrCode = 102,
                ErrMsg = "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable downline's product.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>"
            };
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);
            cacheRefService.GetBetTypeName(2401).Returns("Baccarat, Roulette, Sic Bo, Dragon Tiger");

            // Act
            var actualResult = (await productStatusServiceImpl.UpdateProductStatusAsync(commandParam)).FirstOrDefault();

            // Assert
            Assert.True(!actualResult.IsSuccess &&
                actualResult.Message == "You are not allowed to disable downline's product.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_NoHostPermission_ReturnsForbiddenResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                false,
                30,
                CustIdTest,
                CustRoleTest,
                0,
                UplineLevelTest,
                0,
                string.Empty,
                0,
                null,
                null,
                null,
                "en-US",
                Array.Empty<int>());
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(false);

            // Act
            var actualData = (await productStatusServiceImpl.UpdateProductStatusAsync(commandParam)).FirstOrDefault();

            // Assert
            Assert.Equal(403, actualData.Code);
        }
    }
}