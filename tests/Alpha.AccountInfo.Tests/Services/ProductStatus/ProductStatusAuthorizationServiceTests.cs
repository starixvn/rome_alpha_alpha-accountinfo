namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using System;
    using AccountInfo.Criteria;
    using AccountInfo.Enumerations;
    using AccountInfo.Models;
    using AccountInfo.Services.ProductStatus;
    using Constants;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Params.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusAuthorizationServiceTests
    {
        private const int ProductIdTest = 22;
        private const int CustIdTest = 1176083;
        private const int ViewerIdTest = 1176070;
        private static readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
        private static readonly UserLevel ViewerLevel = Enumeration.FromValue<UserLevel>(4);

        private readonly IDynamicRepository repository;
        private readonly Func<int, bool> canViewProduct;
        private readonly ProductStatusAuthorizationServiceImpl authorizationService;

        public ProductStatusAuthorizationServiceTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            canViewProduct = (productId) => true;
            authorizationService = new ProductStatusAuthorizationServiceImpl(repository, canViewProduct);
        }

        [Theory]
        [InlineData(1176083, 1176070, 3, 4)]
        [InlineData(1176086, 1176083, 2, 3)]
        [InlineData(1176092, 1176086, 1, 2)]
        public void CanView_DirectDownline_ReturnsTrue(int custId, int viewerId, byte custRoleId, byte viewerRoleId)
        {
            // Arrange
            var expectedResult = true;
            var customerStatus = new CustomerStatus(custId, custRoleId, 1176070, 1176083, 1176086, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdTest,
                custId,
                Enumeration.FromValue<UserLevel>(custRoleId),
                viewerId,
                Enumeration.FromValue<UserLevel>(viewerRoleId));

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1176086, 1176070, 2, 4)]
        [InlineData(1176092, 1176070, 1, 4)]
        [InlineData(1176092, 1176083, 1, 3)]
        public void CanView_KenoWithoutDirectDownline_ReturnsTrue(int custId, int viewerId, byte custRoleId, byte viewerRoleId)
        {
            // Arrange
            var expectedResult = true;
            var customerStatus = new CustomerStatus(custId, custRoleId, 1176070, 1176083, 1176086, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdConstants.Keno,
                custId,
                Enumeration.FromValue<UserLevel>(custRoleId),
                viewerId,
                Enumeration.FromValue<UserLevel>(viewerRoleId));

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerStatusIsNull_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            CustomerStatus customerStatus = null;
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerStatusInvalidSMAId_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, CustRoleTest.Value, 0, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerStatusInvalidRole_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, CustRoleTest.Value, 0, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdTest,
                CustIdTest,
                CustRoleTest,
                ViewerIdTest,
                Enumeration.FromValue<UserLevel>(1));

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerStatusInvalidRole_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, CustRoleTest.Value, 0, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdTest,
                CustIdTest,
                CustRoleTest,
                ViewerIdTest,
                Enumeration.FromValue<UserLevel>(1));

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(1)]
        public void CanView_CustomerStatusInvalidRoleId_ReturnsFalse(byte roleId)
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, roleId, ViewerIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CasinoNotSync_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, CustRoleTest.Value, ViewerIdTest, 0, 0, false);
            var param = new ProductStatusAuthorizationParam(ProductIdConstants.RNGCasino, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanUpdate_DirectDownline_ReturnsTrue()
        {
            // Arrange
            var expectedResult = true;
            var customerStatus = new CustomerStatus(CustIdTest, CustRoleTest.Value, ViewerIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanUpdate(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(1)]
        public void CanUpdate_KenoWithoutDirectDownline_ReturnsFalse(byte roleId)
        {
            // Arrange
            var expectedResult = false;
            var customerStatus = new CustomerStatus(CustIdTest, roleId, ViewerIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdConstants.Keno, CustIdTest, CustRoleTest, ViewerIdTest, ViewerLevel);

            repository
                .Get<CustomerStatus>(Arg.Any<SelectCustomerStatusCriteria>())
                .Returns(customerStatus);

            // Act
            var actualResult = authorizationService.CanUpdate(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
