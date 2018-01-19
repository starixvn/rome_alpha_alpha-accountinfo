namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AccountInfo.Criteria.ProductStatus;
    using AccountInfo.Models.ProductStatus;
    using AccountInfo.Services.ProductStatus;
    using Constants;
    using Fanex.Data.Repository;
    using NSubstitute;
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
            dynamicRepository = Substitute.For<IDynamicRepository>();
            cacheRefService = Substitute.For<CacheRefService>();
            criteriaFactory = Substitute.For<ProductStatusCriteriaFactory>();

            productStatusServiceImpl = new ProductStatusServiceImpl(dynamicRepository, cacheRefService, criteriaFactory);
        }

        [Fact]
        public async Task GetProductStatusAsync_Casino_ReturnsDataFromDynamicRepository()
        {
            // Arrange
            var productId = ProductIdConstants.RNGCasino;
            var productName = "Casino";
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 208, 2101, false, false)
            };
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

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
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);
            var actualResult = actualData.ToList();

            // Assert
            Assert.True(
                expectedResult[0].GroupId == actualResult[0].GroupId &&
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
            var criteria = new SelectNG2StatusCriteria(productId, CustIdTest);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

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
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, RoleIdTest);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

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
            var criteria = new SelectDCSProductStatusCriteria(productId, CustIdTest, RoleIdTest);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

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
            var criteria = new SelectProductStatusCriteria(productId, CustIdTest);

            var productStatus = Enumerable.Empty<ProductStatusModel>();
            var expectedResult = Enumerable.Empty<GroupProductStatus>();

            criteriaFactory.CreateSelectCriteria(productId, CustIdTest, RoleIdTest).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, CustIdTest, RoleIdTest);

            // Assert
            Assert.Equal(expectedResult.Any(), actualData.Any());
        }
    }
}
