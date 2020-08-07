namespace Alpha.AccountInfo.Tests.Features.ProductStatusPopup
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Models;
    using Alpha.AccountInfo.Features.ProductStatusPopup.Params.SPs;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared;
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
        private readonly RnGCasinoService rnGCasinoService;
        private readonly ProductStatusServiceImpl productStatusServiceImpl;
        private readonly ILogger logger;

        public ProductStatusServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            criteriaFactory = Substitute.For<ProductStatusCriteriaFactory>();
            authorizationService = Substitute.For<ProductStatusAuthorizationService>();
            rnGCasinoService = Substitute.For<RnGCasinoService>();
            logger = Substitute.For<ILogger>();
            productStatusServiceImpl = new ProductStatusServiceImpl(
                dynamicRepository,
                cacheRefService,
                criteriaFactory,
                authorizationService,
                rnGCasinoService,
                logger);

            rnGCasinoService.SyncedToRnGCasino3rdByStatus(
                new UpdateRnGCasino3rdParam(
                CustIdTest,
                StatusNames.EnableRnGCasino3rd,
                true,
                1176070,
                "User Test",
                "127.0.0.1"));
        }

        [Theory]
        [InlineData(ProductConstants.RNGCasino, "Casino")]
        public async Task GetProductStatusAsync_CasinoColossusBets_ReturnsDataFromDynamicRepository(int productId, string productName)
        {
            // Arrange
            var expectedResults = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 208, 2101, false, false)
                {
                    Name = productName
                }
            };
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
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
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);
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
        public async Task GetProductStatusAsync_Keno_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductConstants.Keno;
            var sportType = 202;
            var expectedResults = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 1501, false, false),
                new ProductStatusModel(productId, sportType, 1503, false, false),
                new ProductStatusModel(productId, sportType, 1504, false, false)
            };
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
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
                .GetBetTypeName(1501)
                .Returns("Australia");
            cacheRefService
                .GetBetTypeName(1503)
                .Returns("Slovakia");

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);
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
        public async Task GetProductStatusAsync_NG2_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductConstants.NG2;
            var productName = "NG2";
            var expectedResults = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, false)
            };
            var criteria = new SelectNG2StatusCriteria(productId, CustIdTest);
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
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);
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
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);
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
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);
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
        public async Task GetProductStatusAsync_NoViewPermission_ReturnsEmptyModel()
        {
            // Arrange
            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(false);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(ProductConstants.RNGCasino, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);

            // Assert
            Assert.False(actualData.Any());
        }

        [Fact]
        public async Task GetProductStatusAsync_NoData_ReturnsEmptyModel()
        {
            // Arrange
            var productId = ProductConstants.RNGCasino;
            var expectedResult = Enumerable.Empty<ProductStatusModel>();

            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, CustRoleTest.Value)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(expectedResult);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest);

            // Assert
            Assert.Equal(expectedResult.Any(), actualData.Any());
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Keno_ReturnsSucessfulResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                ProductConstants.Keno,
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
                "en-US");
            var criteria = new List<UpdateKenoStatusCriteria>
            {
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1501, false, 1176070),
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1503, false, 1176070)
            };
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateKenoStatusCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Keno_ReturnsErrorMessage()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                ProductConstants.Keno,
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
                "en-US");
            var criteria = new List<UpdateKenoStatusCriteria>
            {
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1501, false, 1176070)
                {
                    ErrCode = 102,
                    ErrMsg = "You are not allowed to disable for downlines."
                },
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1503, false, 1176070)
                {
                    ErrCode = 102,
                    ErrMsg = "You are not allowed to disable for downlines."
                }
            };
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateKenoStatusCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);
            cacheRefService.GetBetTypeName(1501).Returns("Australia");
            cacheRefService.GetBetTypeName(1503).Returns("Slovakia");

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(!actualResult.IsSuccess &&
                actualResult.Code == 101 &&
                actualResult.Message == "You are not allowed to disable for downlines.: Australia<br>You are not allowed to disable for downlines.: Slovakia");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Casino_ReturnsSucessfulResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                ProductConstants.RNGCasino,
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
                "en-US");
            var criteria = new UpdateCasinoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false);
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateCasinoStatusCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Casino_ReturnsErrorResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
                ProductConstants.RNGCasino,
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
                "en-US");
            var criteria = new UpdateCasinoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false)
            {
                ErrCode = 102,
                ErrMsg = "You are not allowed to disable for downlines."
            };
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            criteriaFactory
                .CreateUpdateCasinoStatusCriteria(Arg.Any<ProductStatusCommandParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(!actualResult.IsSuccess &&
                actualResult.Message == "You are not allowed to disable for downlines.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsSucessfulResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
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
                "en-US");
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", new[]
            {
                new BetTypeStatusParam(211, 2401, false),
                new BetTypeStatusParam(211, 2402, false)
            });
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
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(actualResult.IsSuccess);
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsErrorResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
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
                "en-US");
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", new[]
            {
                new BetTypeStatusParam(211, 2401, false),
                new BetTypeStatusParam(211, 2402, false)
            })
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
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.True(!actualResult.IsSuccess &&
                actualResult.Message == "You are not allowed to disable downline's product.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_NoHostPermission_ReturnsForbiddenResult()
        {
            // Arrange
            var commandParam = new ProductStatusCommandParam(
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
                "en-US");
            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(false);

            // Act
            var actualData = await productStatusServiceImpl.UpdateProductStatusAsync(commandParam);

            // Assert
            Assert.Equal(403, actualData.Code);
        }
    }
}