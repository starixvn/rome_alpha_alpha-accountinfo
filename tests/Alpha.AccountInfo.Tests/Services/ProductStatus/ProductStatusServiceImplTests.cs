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
            var custId = 1176070;
            byte roleId = 3;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 208, 2101, false, true, false, false, false, true)
            };
            var criteria = new SelectProductStatusCriteria(productId, custId);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);
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
            var custId = 1176070;
            byte roleId = 3;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 1501, false, true, false, false, false, true),
                new ProductStatusModel(productId, sportType, 1503, false, true, false, false, false, true),
                new ProductStatusModel(productId, sportType, 1504, false, true, false, false, false, true)
            };
            var criteria = new SelectProductStatusCriteria(productId, custId);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);
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
            var custId = 1176070;
            byte roleId = 3;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, true, false, false, false, true)
            };
            var criteria = new SelectNG2ProductStatusCriteria(productId, custId);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);
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
            var custId = 1176070;
            byte roleId = 3;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, 212, 4001, false, true, false, false, false, true)
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, custId, roleId);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(productId, productName, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);
            cacheRefService.GetProductName(productId).Returns(productName);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);
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
            var custId = 1176070;
            byte roleId = 3;
            IEnumerable<ProductStatusModel> productStatus = new List<ProductStatusModel>
            {
                new ProductStatusModel(productId, sportType, 2401, false, true, false, false, false, true),
                new ProductStatusModel(productId, sportType, 2402, false, true, false, false, false, true),
            };
            var criteria = new SelectDCSProductStatusCriteria(productId, custId, roleId);
            var expectedResult = new List<GroupProductStatus>
            {
                new GroupProductStatus(sportType, productStatus)
            };
            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);
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
            var custId = 1176070;
            byte roleId = 3;
            var criteria = new SelectProductStatusCriteria(productId, custId);

            var productStatus = Enumerable.Empty<ProductStatusModel>();
            var expectedResult = Enumerable.Empty<GroupProductStatus>();

            criteriaFactory.CreateSelectCriteria(productId, custId, roleId).Returns(criteria);
            dynamicRepository.FetchAsync<ProductStatusModel>(criteria).Returns(productStatus);

            // Act
            var actualData = await productStatusServiceImpl.GetProductStatusAsync(productId, custId, roleId);

            // Assert
            Assert.Equal(expectedResult.Any(), actualData.Any());
        }
    }
}
