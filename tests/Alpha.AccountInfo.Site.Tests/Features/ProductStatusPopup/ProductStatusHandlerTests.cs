//namespace Alpha.AccountInfo.Site.Tests.Features.ProductStatusPopup
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using AccountInfo.Models.ProductStatus;
//    using Alpha.AccountInfo.Features.Shared.Models;
//    using Alpha.AccountInfo.Site.Features.ProductStatusPopup;
//    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests;
//    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels;
//    using Alpha.Products.Core.Shared;
//    using NSubstitute;
//    using Params.ProductStatus;
//    using Services.ProductStatus;
//    using Xunit;

//    [Trait("Category", "ProductStatus")]
//    public class ProductStatusHandlerTests
//    {
//        private const int UplineIdTest = 1176070;
//        private const bool IsInternalTest = false;
//        private const bool CanUpdateAccountTest = true;
//        private const int ProductIdTest = 22;
//        private const int CustIdTest = 1176083;
//        private static readonly UserLevel CustRoleTest = Enumeration.FromValue<UserLevel>(3);
//        private static readonly UserLevel UplineLevelTest = Enumeration.FromValue<UserLevel>(4);

//        private readonly IUser user;
//        private readonly ProductStatusService statusService;
//        private readonly ProductStatusHandler statusHandler;

//        public ProductStatusHandlerTests()
//        {
//            user = Substitute.For<IUser>();
//            statusService = Substitute.For<ProductStatusService>();
//            statusHandler = new ProductStatusHandler(statusService);
//        }

//        [Fact]
//        public async Task HandleProductStatusQuery_Always_ReturnsCorrectViewModel()
//        {
//            // Arrange
//            user.Id.Returns(UplineIdTest);
//            user.LevelId.Returns(UplineLevelTest.Value);
//            user.IsInternal.Returns(IsInternalTest);
//            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
//            var message = new ProductStatusQuery(user)
//            {
//                ProductId = ProductIdTest,
//                CustId = CustIdTest,
//                CustRoleId = CustRoleTest.Value
//            };
//            var productStatus = new List<ProductStatusModel>
//            {
//                new ProductStatusModel(ProductIdTest, 207, 1004, false, false)
//            };
//            var expectedViewModel = new ProductStatusViewModel(ProductIdTest, CustIdTest, null, CustRoleTest, productStatus, UplineLevelTest, false);

//            statusService
//                .GetProductStatusAsync(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest)
//                .Returns(productStatus);

//            // Act
//            var actualViewModel = await statusHandler.Handle(message);

//            // Assert
//            Assert.True(
//                expectedViewModel.ProductId == actualViewModel.ProductId &&
//                expectedViewModel.CustId == actualViewModel.CustId &&
//                expectedViewModel.CustName == actualViewModel.CustName &&
//                expectedViewModel.CustRole == actualViewModel.CustRole &&
//                expectedViewModel.ProductStatus.Count() == actualViewModel.ProductStatus.Count() &&
//                expectedViewModel.IsAgentUpdateMember == actualViewModel.IsAgentUpdateMember &&
//                expectedViewModel.IsDirectDownline == actualViewModel.IsDirectDownline &&
//                expectedViewModel.IsViewOnly == actualViewModel.IsViewOnly);
//        }

//        [Fact]
//        public async Task HandleProductStatusQuery_NoViewPermission_ReturnsNull()
//        {
//            // Arrange
//            user.Id.Returns(UplineIdTest);
//            user.LevelId.Returns(UplineLevelTest.Value);
//            user.IsInternal.Returns(IsInternalTest);
//            user.CanUpdateAccount.Returns(CanUpdateAccountTest);
//            var message = new ProductStatusQuery(user)
//            {
//                ProductId = ProductIdTest,
//                CustId = CustIdTest,
//                CustRoleId = CustRoleTest.Value
//            };

//            statusService
//                .GetProductStatusAsync(ProductIdTest, CustIdTest, CustRoleTest, UplineIdTest, UplineLevelTest)
//                .Returns(Enumerable.Empty<ProductStatusModel>());

//            // Act
//            var actualViewModel = await statusHandler.Handle(message);

//            // Assert
//            Assert.False(actualViewModel.ProductStatus.Any());
//        }

//        [Fact]
//        public async Task HandleProductStatusCommand_Always_ReturnsSuccessResult()
//        {
//            // Arrange
//            user.Id.Returns(UplineIdTest);
//            user.LevelId.Returns(UplineLevelTest.Value);
//            var message = new ProductStatusCommand(user)
//            {
//                ProductId = ProductConstants.Keno,
//                CustId = CustIdTest,
//                CustRoleId = CustRoleTest.Value,
//                BetTypeStatus = new[]
//                {
//                    new BetTypeStatus { SportType = 207, BetType = 1004, IsDisabled = true }
//                }
//            };
//            var expectedResult = Result.CreateSuccessResult();

//            statusService
//                .UpdateProductStatusAsync(Arg.Any<ProductStatusCommandParam>())
//                .Returns(expectedResult);

//            // Act
//            var actualResult = await statusHandler.Handle(message);

//            // Assert
//            Assert.True(expectedResult.Code == actualResult.Code);
//        }

//        [Fact]
//        public async Task HandleProductStatusCommand_NoHostPermission_ReturnsForbidden()
//        {
//            // Arrange
//            user.Id.Returns(UplineIdTest);
//            user.LevelId.Returns(UplineLevelTest.Value);
//            var message = new ProductStatusCommand(user)
//            {
//                ProductId = ProductConstants.Keno,
//                CustId = CustIdTest,
//                CustRoleId = CustRoleTest.Value,
//                BetTypeStatus = new[]
//                {
//                    new BetTypeStatus { SportType = 207, BetType = 1004, IsDisabled = true }
//                }
//            };
//            var expectedResult = Result.CreateForbiddenResult();

//            statusService
//                .UpdateProductStatusAsync(Arg.Any<ProductStatusCommandParam>())
//                .Returns(expectedResult);

//            // Act
//            var actualResult = await statusHandler.Handle(message);

//            // Assert
//            Assert.Equal(403, actualResult.Code);
//        }
//    }
//}