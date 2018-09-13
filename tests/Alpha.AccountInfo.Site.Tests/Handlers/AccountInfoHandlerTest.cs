
namespace Alpha.AccountInfo.Site.Tests.Handlers
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Params.ProductStatus;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Handlers;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.ViewModels.BalanceInfo;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using Alpha.Products.Core.Services;
    using AutoFixture;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "AccountInfoHandler")]
    public class AccountInfoHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo  = AccountInfoHelper.BuildCustomerInfo();
        private readonly AccountInfoService accountInfoService;
        private readonly AccountInfoHandler accountInfoHandler;
        private readonly Fixture fixture = new Fixture();
        private readonly CustomerInfoQuery customerInfoQuery;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly CompareLogic compareLogic = new CompareLogic();

        public AccountInfoHandlerTest()
        {
            getBalanceInfoViewModelByLevel =
               new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
               {
                    {UserLevel.Master, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Agent, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
               };

            accountInfoService = Substitute.For<AccountInfoService>();
            accountInfoHandler = new AccountInfoHandler(accountInfoService, getBalanceInfoViewModelByLevel);

            customerInfoQuery = new CustomerInfoQuery(viewer)
            {
                UserName = "test"
            };

            SimpleServiceLocator
             .Instance
             .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
             .Register<Func<string, ResourceManager, string>>((language, resources) => "{}");
        }

        [Fact]
        public async Task HandleAccountInfoQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new AccountInfoQuery(viewer);
            var expectedViewModel = new AccountInfoViewModel(message);

            var actualViewModel = await accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUserNameSuggestionQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new UserNameSuggestionQuery(viewer)
            {
                SuggestionQuery = "test"
            };

            var expectedViewModel = Enumerable.Empty<UsernameSuggestion>();
            accountInfoService.GetUsernamesSuggestion(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(expectedViewModel);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleDownlineListQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new DownlineListQuery(viewer)
            {
                SearchAccountId = 1
            };

            var model = new DownlineListModel(
                Enumerable.Empty<string>(),
                totalAgActive: 12,
                totalAgClosed: 13,
                totalAgSuspended: 14,
                totalMemActive: 15,
                totalMemClosed: 16,
                totalMemSuspended: 17);
            var expectedViewModel = new DownlineListViewModel(model);
            accountInfoService.GetDownlineListModel(1).Returns(model);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleCustomerInfoQuery_Always_CallGetCustomerInfoModel()
        {
            var actualViewModel = accountInfoHandler.Handle(customerInfoQuery);

            accountInfoService.Received().GetCustomerInfoModel(Arg.Any<string>(), Arg.Any<int>());
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoIsNull_ReturnFalse()
        {
            CustomerInfoModel nullCustomerInfo = null;
            accountInfoService.GetCustomerInfoModel(Arg.Any<string>(), Arg.Any<int>()).Returns(nullCustomerInfo);

            var actualViewModel = accountInfoHandler.Handle(customerInfoQuery);

            Assert.Null(actualViewModel);
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoHasData_ReturnCorrectViewModel()
        {
            var memberBalanceInfoViewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(customerInfoQuery, customerInfo);
            accountInfoService.GetCustomerInfoModel(Arg.Any<string>(), Arg.Any<int>()).Returns(customerInfo);
            accountInfoService.GetBalanceInfoModel(Arg.Any<int>(), Arg.Any<byte>()).Returns(AccountInfoHelper.MemberBalanceInfo);
            var expectedViewModel = new CustomerInfoViewModel(
                customerInfoQuery, 
                customerInfo,
                memberBalanceInfoViewModel);

            var actualViewModel = accountInfoHandler.Handle(customerInfoQuery);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoHasData_CallGetBalanceInfoModel()
        {
            accountInfoService.GetCustomerInfoModel(Arg.Any<string>(), Arg.Any<int>()).Returns(customerInfo);

            var actualViewModel = accountInfoHandler.Handle(customerInfoQuery);

            accountInfoService.Received().GetBalanceInfoModel(Arg.Any<int>(), Arg.Any<byte>());
        }

        [Fact]
        public void HandleOnlineListQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new OnlineListQuery(viewer)
            {
                SearchAccountId = 1
            };

            var model = Enumerable.Empty<OnlineUserModel>();
            accountInfoService.GetOnlineList(1).Returns(model);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public async Task HandleGivenCreditQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new GivenCreditQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "test",
                SearchAccountRoleId = 2
            };
            var expectedViewModel = new GivenCreditViewModel(message);

            var actualViewModel = await accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }


        [Fact]
        public void HandleGivenCreditCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new GivenCreditCommand(viewer)
            {
                SearchAccountId = 1,
                CreditAmount = 1000,
                SearchAccountRoleId = 1
            };

            var model = Result.CreateSuccessResult();
            accountInfoService.UpdateGivenCredit(Arg.Any<UpdateGivenCreditParam>()).Returns(model);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public void HandleCustomerStatusCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            var model = Result.CreateSuccessResult();
            accountInfoService.UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>()).Returns(model);

            var actualViewModel = accountInfoHandler.Handle(message);

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public void HandleCustomerAllStatusQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerAllStatusQuery(viewer)
            {
                SearchAccountId = 1
            };

            var model = fixture.Create<CustomerAllStatusModel>();
            accountInfoService.GetCustomerAllStatusModel(Arg.Any<int>()).Returns(model);
            var expectedViewModel = new CustomererAllStatusViewModel(message, model);

            var actualViewModel = accountInfoHandler.Handle(message);

            Assert.True(compareLogic.Compare(actualViewModel, expectedViewModel).AreEqual);
        }

        private IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
           CustomerInfoQuery message,
           CustomerInfoModel customerInfo,
           IBalanceInfoModel balanceInfoModel) 
            => AccountInfoHelper.BuildMemberBalanceInfoViewModel(
               customerInfoQuery, 
               customerInfo);
    }
}
