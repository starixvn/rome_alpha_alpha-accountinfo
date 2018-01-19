namespace Alpha.AccountInfo.Site.Tests.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AccountInfo.Models;
    using AccountInfo.Models.ProductStatus;
    using Constants;
    using Enumerations;
    using NSubstitute;
    using Requests.ProductStatus;
    using Services.ProductStatus;
    using Site.Handlers;
    using ViewModels.ProductStatus;
    using Xunit;

    [Trait("Category", "ProductStatus")]
    public class ProductStatusHandlerTests
    {
        private readonly IUser user;
        private readonly ProductStatusService productStatusService;
        private readonly ProductStatusHandler productStatusHandler;

        public ProductStatusHandlerTests()
        {
            user = Substitute.For<IUser>();
            productStatusService = Substitute.For<ProductStatusService>();
            productStatusHandler = new ProductStatusHandler(user, productStatusService);
        }

        [Fact]
        public async Task HandleProductStatusQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            user.LevelId.Returns((byte)3);
            user.IsInternal.Returns(false);
            user.CanUpdateAccount.Returns(true);

            var message = new ProductStatusQuery
            {
                ProductId = ProductIdConstants.RNGCasino,
                CustId = 1176083,
                CustName = "master1",
                CustRoleId = 3
            };
            var expectedData = new List<GroupProductStatus>
            {
                new GroupProductStatus(message.ProductId, "Casino", new List<ProductStatusModel>
                {
                    new ProductStatusModel(message.ProductId, 208, 2101, false, true, false, false, false, true)
                })
            };
            var expectedViewModel = new ProductStatusViewModel(
                message.ProductId,
                message.CustId,
                message.CustName,
                Enumeration.FromValue<UserLevel>(message.CustRoleId),
                expectedData,
                Enumeration.FromValue<UserLevel>(user.LevelId),
                user.IsInternal || !user.CanUpdateAccount);

            productStatusService
                .GetProductStatus(message.ProductId, message.CustId, message.CustRoleId)
                .Returns(expectedData);

            // Act
            var actualViewModel = await productStatusHandler.Handle(message);

            // Assert
            Assert.True(
                expectedViewModel.ProductId == actualViewModel.ProductId &&
                expectedViewModel.CustId == actualViewModel.CustId &&
                expectedViewModel.CustName == actualViewModel.CustName &&
                expectedViewModel.CustRole == actualViewModel.CustRole &&
                expectedViewModel.IsAgentUpdateMember == actualViewModel.IsAgentUpdateMember &&
                expectedViewModel.IsDirectDownline == actualViewModel.IsDirectDownline &&
                expectedViewModel.IsReadOnly == actualViewModel.IsReadOnly);
        }
    }
}
