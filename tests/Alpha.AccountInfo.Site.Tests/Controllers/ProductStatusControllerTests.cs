namespace Alpha.AccountInfo.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Constants;
    using MediatR;
    using NSubstitute;
    using Requests.ProductStatus;
    using Site.Controllers;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusControllerTests
    {
        private readonly IMediator mediator;
        private readonly ProductStatusController productStatusController;

        public ProductStatusControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            productStatusController = new ProductStatusController(mediator);
        }

        [Fact]
        public async Task HandleProductStatusQuery_Always_CallsMediatorSendProductStatusQuery()
        {
            // Arrange
            var message = new ProductStatusQuery
            {
                ProductId = ProductIdConstants.RNGCasino,
                CustId = 1176083,
                CustName = "master1",
                CustRoleId = 3
            };

            // Act
            await productStatusController.Index(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Theory]
        [InlineData(int.MinValue, 1176083, 3)]
        [InlineData(0, 1176083, 3)]
        [InlineData(ProductIdConstants.RNGCasino, int.MinValue, 3)]
        [InlineData(ProductIdConstants.RNGCasino, 0, 3)]
        [InlineData(ProductIdConstants.RNGCasino, 1176083, 4)]
        public async Task HandleProductStatusQuery_InvalidQuery_ReturnsInvalidPageView(int productId, int custId, byte custRoleId)
        {
            // Arrange
            var message = new ProductStatusQuery
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
    }
}
