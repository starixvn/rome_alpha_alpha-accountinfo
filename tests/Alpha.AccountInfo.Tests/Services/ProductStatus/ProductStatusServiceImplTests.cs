namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;
    using AccountInfo.Configurations;
    using AccountInfo.Criteria.ProductStatus;
    using AccountInfo.Models.ProductStatus;
    using AccountInfo.Services.ProductStatus;
    using Constants;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Params.ProductStatus;
    using RemotingRef;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusServiceImplTests
    {
        private const int CustIdTest = 1176083;
        private const byte RoleIdTest = 3;

        private readonly IDynamicRepository dynamicRepository;
        private readonly CacheRefService cacheRefService;
        private readonly ProductStatusCriteriaFactory criteriaFactory;
        private ProductStatusServiceImpl productStatusServiceImpl;

        public ProductStatusServiceImplTests()
        {
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["SystemId"] = "2"
            };

            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            criteriaFactory = Substitute.For<ProductStatusCriteriaFactory>();

            productStatusServiceImpl = new ProductStatusServiceImpl(dynamicRepository, cacheRefService, criteriaFactory);
        }

        [Theory]
        [InlineData(ProductIdConstants.RNGCasino, "Casino")]
        [InlineData(ProductIdConstants.ColossusBets, "Colossus Bets")]
        public async Task GetProductStatusAsync_CasinoColossusBets_ReturnsDataFromDynamicRepository(int productId, string productName)
        {
            // Arrange
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 208, 2101, false, false)
            };
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);
            cacheRefService
                .GetProductName(productId)
                .Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.First();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult.GroupId &&
                expectedResult[0].ProductName == actualResult.ProductName &&
                expectedResult[0].ProductStatuses.Count() == actualResult.ProductStatuses.Count());
        }

        [Fact]
        public async Task GetProductStatusAsync_Keno_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductIdConstants.Keno;
            var sportType = 202;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 1501, false, false),
                new ProductStatusModel(productId, sportType, 1503, false, false),
                new ProductStatusModel(productId, sportType, 1504, false, false)
            };
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);
            cacheRefService
                .GetBetTypeName(1501)
                .Returns("Australia");
            cacheRefService
                .GetBetTypeName(1503)
                .Returns("Slovakia");
            cacheRefService
                .GetBetTypeName(1504)
                .Returns("Canada");

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.ToList();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult[0].GroupId &&
                expectedResult[0].GroupName == actualResult[0].GroupName &&
                expectedResult[0].ProductStatuses.Count() == actualResult[0].ProductStatuses.Count());
        }

        [Fact]
        public async Task GetProductStatusAsync_NG2_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductIdConstants.NG2;
            var productName = "NG2";
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, false)
            };
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            var criteria = new SelectNG2StatusCriteria(productId, CustIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);
            cacheRefService
                .GetProductName(productId)
                .Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.First();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult.GroupId &&
                expectedResult[0].ProductName == actualResult.ProductName &&
                expectedResult[0].ProductStatuses.Count() == actualResult.ProductStatuses.Count());
        }

        [Fact]
        public async Task GetProductStatusAsync_DCSSingleBetType_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductIdConstants.MacauGames;
            var productName = "Macau Games";
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, false)
            };
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, RoleIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);
            cacheRefService
                .GetProductName(productId)
                .Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.First();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult.GroupId &&
                expectedResult[0].ProductName == actualResult.ProductName &&
                expectedResult[0].ProductStatuses.Count() == actualResult.ProductStatuses.Count());
        }

        [Fact]
        public async Task GetProductStatusAsync_DCSMultipleBetTypes_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductIdConstants.Allbet;
            var sportType = 211;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 2401, false, false),
                new ProductStatusModel(productId, sportType, 2402, false, false)
            };
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, RoleIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.ToList();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult[0].GroupId &&
                expectedResult[0].ProductStatuses.Count() == actualResult[0].ProductStatuses.Count());
        }

        [Fact]
        public async Task GetProductStatusAsync_NoData_ReturnsEmptyModel()
        {
            // Arrange
            var productId = ProductIdConstants.RNGCasino;

            var productStatus = Enumerable.Empty<ProductStatusModel>();
            var expectedResult = Enumerable.Empty<GroupProductStatus>();

            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            criteriaFactory
                .CreateSelectCriteria(productId, CustIdTest, RoleIdTest)
                .Returns(criteria);
            dynamicRepository
                .FetchAsync<ProductStatusModel>(criteria)
                .Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);

            // Assert
            Assert.Equal(expectedResult.Any(), actualData.Any());
        }

        [Fact]
        public async Task UpdateKenoStatusAsync_ValidCriteria_ReturnsSucessfulResult()
        {
            // Arrange
            var criteria = new List<UpdateKenoStatusCriteria>
            {
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1501, false, 1176070),
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1503, false, 1176070)
            };
            criteriaFactory
                .CreateUpdateKenoStatusCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateKenoStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(actualResult.IsSuccess());
        }

        [Fact]
        public async Task UpdateKenoStatusAsync_ValidCriteria_ReturnsErrorMessage()
        {
            // Arrange
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
            criteriaFactory
                .CreateUpdateKenoStatusCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateKenoStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(!actualResult.IsSuccess() &&
                actualResult.Code == 102 &&
                actualResult.Message == "You are not allowed to disable for downlines.");
        }

        [Fact]
        public async Task UpdateKenoStatusAsync_ValidCriteria_ReturnsGroupErrorMessage()
        {
            // Arrange
            var criteria = new List<UpdateKenoStatusCriteria>
            {
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1501, false, 1176070)
                {
                    ErrCode = 102,
                    ErrMsg = "You are not allowed to disable for downlines."
                },
                new UpdateKenoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", 1503, false, 1176070)
                {
                    ErrCode = 500,
                    ErrMsg = "Internal Server Error."
                }
            };
            criteriaFactory
                .CreateUpdateKenoStatusCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateKenoStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(!actualResult.IsSuccess() &&
                actualResult.Message == "You are not allowed to disable for downlines.<br/>Internal Server Error.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Casino_ReturnsSucessfulResult()
        {
            // Arrange
            var criteria = new UpdateCasinoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false);
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(actualResult.IsSuccess());
        }

        [Fact]
        public async Task UpdateProductStatusAsync_Casino_ReturnsErrorResult()
        {
            // Arrange
            var criteria = new UpdateCasinoStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false)
            {
                ErrCode = 102,
                ErrMsg = "You are not allowed to disable for downlines."
            };
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(!actualResult.IsSuccess() &&
                actualResult.Message == "You are not allowed to disable for downlines.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_ColossusBets_ReturnsSucessfulResult()
        {
            // Arrange
            var criteria = new UpdateColossusBetStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false);
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(actualResult.IsSuccess());
        }

        [Fact]
        public async Task UpdateProductStatusAsync_ColossusBets_ReturnsErrorResult()
        {
            // Arrange
            var criteria = new UpdateColossusBetStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", "E", false)
            {
                ErrCode = 102,
                ErrMsg = "You are not allowed to disable for downlines."
            };
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(!actualResult.IsSuccess() &&
                actualResult.Message == "You are not allowed to disable for downlines.");
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsSucessfulResult()
        {
            // Arrange
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", new[]
            {
                new ProductStatusParam(211, 2401, false),
                new ProductStatusParam(211, 2402, false)
            });
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(actualResult.IsSuccess());
        }

        [Fact]
        public async Task UpdateProductStatusAsync_DSC_ReturnsErrorResult()
        {
            // Arrange
            var criteria = new UpdateDCSProductStatusCriteria(CustIdTest, 536751, 1176070, "127.0.0.1", new[]
            {
                new ProductStatusParam(211, 2401, false),
                new ProductStatusParam(211, 2402, false)
            })
            {
                ErrCode = 102,
                ErrMsg = "<Root MsgID=\"60035\" Msg=\"You are not allowed to disable for downlines.\"><Row SportType=\"211\" BetType=\"2401\"/></Root>"
            };
            criteriaFactory
                .CreateUpdateCriteria(Arg.Any<UpdateProductStatusParam>())
                .Returns(criteria);
            dynamicRepository
                .ExecuteAsync(Arg.Any<INonQueryCommand>())
                .Returns(Task.CompletedTask);

            // Act
            var actualResult = await productStatusServiceImpl.UpdateProductStatusAsync(Arg.Any<UpdateProductStatusParam>());

            // Assert
            Assert.True(!actualResult.IsSuccess() &&
                actualResult.Message == "You are not allowed to disable for downlines.");
        }
    }
}
