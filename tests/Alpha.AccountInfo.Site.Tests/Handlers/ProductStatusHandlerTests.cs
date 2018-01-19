namespace Alpha.AccountInfo.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;
    using Constants;
    using Enumerations;
    using NSubstitute;
    using Params.ProductStatus;
    using Requests.ProductStatus;
    using Services.ProductStatus;
    using Site.Handlers;
    using ViewModels.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusHandlerTests
    {
        private const int ViewerIdTest = 1176070;
        private const bool IsInternalTest = false;
        private const bool CanUpdateAccountTest = true;
        private const int ProductIdTest = ProductIdConstants.Bullfighting;
        private const int CustIdTest = 1176083;
        private static readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
        private static readonly UserLevel ViewerLevelTest = Enumeration.FromValue<UserLevel>(4);

        private readonly IUser user;
        private readonly ProductStatusService statusService;
        private readonly ProductStatusAuthorizationService authorizationService;
        private readonly ProductStatusHandler statusHandler;

        public ProductStatusHandlerTests()
        {
            user = Substitute.For<IUser>();
            statusService = Substitute.For<ProductStatusService>();
            authorizationService = Substitute.For<ProductStatusAuthorizationService>();
            statusHandler = new ProductStatusHandler(statusService, authorizationService);
        }

        [Fact]
        public async Task HandleProductStatusQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(ViewerLevelTest.Value);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusQuery(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleTest.Value
            };
            var groupProductStatus = new List<GroupProductStatus>
            {
                new GroupProductStatus(
                    ProductIdTest,
                    string.Empty,
                    new List<ProductStatusModel>
                    {
                        new ProductStatusModel(ProductIdTest, 207, 1004, false, false)
                    })
            };
            var expectedViewModel = new ProductStatusViewModel(ProductIdTest, CustIdTest, null, CustRoleTest, groupProductStatus, ViewerLevelTest, false);

            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            statusService
                .GetProductStatusAsync(ProductIdTest, CustIdTest, CustRoleTest.Value)
                .Returns(groupProductStatus);

            // Act
            var actualViewModel = await statusHandler.Handle(message);

            // Assert
            Assert.True(
                expectedViewModel.ProductId == actualViewModel.ProductId &&
                expectedViewModel.CustId == actualViewModel.CustId &&
                expectedViewModel.CustName == actualViewModel.CustName &&
                expectedViewModel.CustRole == actualViewModel.CustRole &&
                expectedViewModel.GroupProductStatus.Count() == actualViewModel.GroupProductStatus.Count() &&
                expectedViewModel.IsAgentUpdateMember == actualViewModel.IsAgentUpdateMember &&
                expectedViewModel.IsDirectDownline == actualViewModel.IsDirectDownline &&
                expectedViewModel.IsViewOnly == actualViewModel.IsViewOnly);
        }

        [Fact]
        public async Task HandleProductStatusQuery_NoViewPermission_ReturnsNull()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(ViewerLevelTest.Value);
            var message = new ProductStatusQuery(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleTest.Value
            };

            authorizationService
                .CanView(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(false);

            // Act
            var actualViewModel = await statusHandler.Handle(message);

            // Assert
            Assert.Null(actualViewModel);
        }

        [Fact]
        public async Task HandleProductStatusCommand_Keno_ReturnsSuccessResult()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(ViewerLevelTest.Value);
            var message = new ProductStatusCommand(user)
            {
                ProductId = ProductIdConstants.Keno,
                CustId = CustIdTest,
                CustRoleId = CustRoleTest.Value,
                ProductStatus = "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]"
            };
            var expectedResult = Result.CreateSuccessResult();

            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(true);
            statusService
                .UpdateKenoStatusAsync(Arg.Any<UpdateProductStatusParam>())
                .Returns(expectedResult);

            // Act
            var actualResult = await statusHandler.Handle(message);

            // Assert
            Assert.True(expectedResult.Code == actualResult.Code);
        }

        [Fact]
        public async Task HandleProductStatusCommand_NoHostPermission_ReturnsForbidden()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(ViewerLevelTest.Value);
            var message = new ProductStatusCommand(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleTest.Value
            };

            authorizationService
                .CanUpdate(Arg.Any<ProductStatusAuthorizationParam>())
                .Returns(false);

            // Act
            var actualResult = await statusHandler.Handle(message);

            // Assert
            Assert.Equal(403, actualResult.Code);
        }
    }
}
