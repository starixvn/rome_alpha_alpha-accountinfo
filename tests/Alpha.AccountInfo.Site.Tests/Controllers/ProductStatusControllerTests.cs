namespace Alpha.AccountInfo.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AccountInfo.Models;
    using Enumerations;
    using MediatR;
    using NSubstitute;
    using Requests.ProductStatus;
    using Site.Controllers;
    using ViewModels.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusControllerTests
    {
        private const int ViewerIdTest = 1176070;
        private const byte LevelIdTest = 4;
        private const bool IsInternalTest = false;
        private const bool CanUpdateAccountTest = true;
        private const int ProductIdTest = 22;
        private const int CustIdTest = 1176083;
        private const byte CustRoleIdTest = 3;

        private readonly IUser user;
        private readonly IMediator mediator;
        private readonly ProductStatusController productStatusController;

        public ProductStatusControllerTests()
        {
            user = Substitute.For<IUser>();
            mediator = Substitute.For<IMediator>();
            productStatusController = new ProductStatusController(mediator);
        }

        [Fact]
        public async Task HandleProductStatusQuery_Always_CallsMediatorSendProductStatusQuery()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(LevelIdTest);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusQuery(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleIdTest
            };

            // Act
            await productStatusController.Index(message);

            // Assert
            await mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(int.MinValue, CustIdTest, CustRoleIdTest)]
        [InlineData(0, CustIdTest, CustRoleIdTest)]
        [InlineData(ProductIdTest, int.MinValue, CustRoleIdTest)]
        [InlineData(ProductIdTest, 0, CustRoleIdTest)]
        [InlineData(ProductIdTest, CustIdTest, 4)]
        public async Task Index_InvalidQuery_ReturnsInvalidPageView(int productId, int custId, byte custRoleId)
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(LevelIdTest);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusQuery(user)
            {
                ProductId = productId,
                CustId = custId,
                CustRoleId = custRoleId
            };

            // Act
            var actualView = await productStatusController.Index(message) as ViewResult;

            // Assert
            Assert.Equal("~/Views/Shared/NoInformation.cshtml", actualView.ViewName);
        }

        [Fact]
        public async Task Index_ValidViewModel_ReturnsView()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(LevelIdTest);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusQuery(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleIdTest
            };
            var viewModel = new ProductStatusViewModel(
                ProductIdTest, 
                CustIdTest,
                null,
                Enumeration.FromValue<UserLevel>(CustRoleIdTest),
                null,
                Enumeration.FromValue<UserLevel>(4),
                false);
            mediator
                .Send(message)
                .Returns(Task.FromResult<ProductStatusViewModel>(viewModel));

            // Act
            var actualView = await productStatusController.Index(message) as ViewResult;

            // Assert
            Assert.NotNull(actualView);
        }

        [Fact]
        public async Task Update_Always_CallsMediatorSendProductStatusCommand()
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(LevelIdTest);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusCommand(user)
            {
                ProductId = ProductIdTest,
                CustId = CustIdTest,
                CustRoleId = CustRoleIdTest,
                ProductStatus = "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]"
            };

            // Act
            await productStatusController.Update(message);

            // Assert
            await mediator.Received().Send<Result>(message);
        }

        [Theory]
        [InlineData(int.MinValue, CustIdTest, CustRoleIdTest, "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]")]
        [InlineData(0, CustIdTest, CustRoleIdTest, "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]")]
        [InlineData(ProductIdTest, int.MinValue, CustRoleIdTest, "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]")]
        [InlineData(ProductIdTest, 0, CustRoleIdTest, "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]")]
        [InlineData(ProductIdTest, CustIdTest, 4, "[{\"ProductId\":30,\"SportType\":207,\"BetType\":1004,\"IsDisabled\":true}]")]
        [InlineData(ProductIdTest, CustIdTest, CustRoleIdTest, null)]
        [InlineData(ProductIdTest, CustIdTest, CustRoleIdTest, "")]
        public async Task Update_InvalidQuery_ReturnsInvalidPageView(int productId, int custId, byte custRoleId, string productStatus)
        {
            // Arrange
            user.Id.Returns(ViewerIdTest);
            user.LevelId.Returns(LevelIdTest);
            user.IsInternal.Returns(IsInternalTest);
            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
            var message = new ProductStatusCommand(user)
            {
                ProductId = productId,
                CustId = custId,
                CustRoleId = custRoleId,
                ProductStatus = productStatus
            };

            // Act
            var actualResult = await productStatusController.Update(message) as JsonResult;
            var actualData = (Result)actualResult.Data;

            // Assert
            Assert.True(!actualData.IsSuccess());
        }
    }
}
