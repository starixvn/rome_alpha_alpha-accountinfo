namespace Alpha.AccountInfo.Tests.Services.ProductStatus
{
    using AccountInfo.Criteria;
    using AccountInfo.Models;
    using AccountInfo.Services.ProductStatus;
    using Alpha.Products.Core.Shared;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Params.ProductStatus;
    using System;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusAuthorizationServiceTests
    {
        private const int ProductIdTest = 22;
        private const int CustIdTest = 1176083;
        private const int UplineIdTest = 1176070;
        private static readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
        private static readonly UserLevel UplineLevel = Enumeration.FromValue<UserLevel>(4);

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
        public void CanView_DirectDownline_ReturnsTrue(int custId, int uplineId, byte custRoleId, byte uplineRoleId)
        {
            // Arrange
            var expectedResult = true;
            var customerInformation = new CustomerInformation(custId, custRoleId, 1176070, 1176083, 1176086, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdTest,
                custId,
                Enumeration.FromValue<UserLevel>(custRoleId),
                uplineId,
                Enumeration.FromValue<UserLevel>(uplineRoleId));

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(1176086, 1176070, 2, 4)]
        [InlineData(1176092, 1176070, 1, 4)]
        [InlineData(1176092, 1176083, 1, 3)]
        public void CanView_KenoWithoutDirectDownline_ReturnsTrue(int custId, int uplineId, byte custRoleId, byte uplineRoleId)
        {
            // Arrange
            var expectedResult = true;
            var customerInformation = new CustomerInformation(custId, custRoleId, 1176070, 1176083, 1176086, true);
            var param = new ProductStatusAuthorizationParam(
                ProductConstants.Keno,
                custId,
                Enumeration.FromValue<UserLevel>(custRoleId),
                uplineId,
                Enumeration.FromValue<UserLevel>(uplineRoleId));

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerInformationIsNull_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            CustomerInformation customerInformation = null;
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerInformationInvalidSMAId_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerInformation = new CustomerInformation(CustIdTest, CustRoleTest.Value, 0, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CanView_CustomerInformationInvalidRole_ReturnsFalse()
        {
            // Arrange
            var expectedResult = false;
            var customerInformation = new CustomerInformation(CustIdTest, CustRoleTest.Value, 0, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(
                ProductIdTest,
                CustIdTest,
                CustRoleTest,
                UplineIdTest,
                Enumeration.FromValue<UserLevel>(1));

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanView(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(1)]
        public void CanView_CustomerInformationInvalidRoleId_ReturnsFalse(byte roleId)
        {
            // Arrange
            var expectedResult = false;
            var customerInformation = new CustomerInformation(CustIdTest, roleId, UplineIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

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
            var customerInformation = new CustomerInformation(CustIdTest, CustRoleTest.Value, UplineIdTest, 0, 0, false);
            var param = new ProductStatusAuthorizationParam(ProductConstants.RNGCasino, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

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
            var customerInformation = new CustomerInformation(CustIdTest, CustRoleTest.Value, UplineIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

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
            var customerInformation = new CustomerInformation(CustIdTest, roleId, UplineIdTest, 0, 0, true);
            var param = new ProductStatusAuthorizationParam(ProductConstants.Keno, CustIdTest, CustRoleTest, UplineIdTest, UplineLevel);

            repository
                .Get<CustomerInformation>(Arg.Any<SelectCustomerInformationCriteria>())
                .Returns(customerInformation);

            // Act
            var actualResult = authorizationService.CanUpdate(param);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
