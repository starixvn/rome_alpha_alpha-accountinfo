namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Handlers;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Alpha.Products.Core.Services;
    using AutoFixture;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo();
        private readonly SearchAccountService accountInfoService;
        private readonly SearchAccountHandler accountInfoHandler;
        private readonly Fixture fixture = new Fixture();
        private readonly CustomerInfoQuery customerInfoQuery;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly CompareLogic compareLogic = new CompareLogic();

        public SearchAccountHandlerTest()
        {
            getBalanceInfoViewModelByLevel =
               new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
               {
                    {UserLevel.Master, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Agent, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
               };

            accountInfoService = Substitute.For<SearchAccountService>();
            accountInfoHandler = new SearchAccountHandler(accountInfoService, getBalanceInfoViewModelByLevel);

            customerInfoQuery = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = "test"
            };

            SimpleServiceLocator
             .Instance
             .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
             .Register<Func<string, ResourceManager, string>>((language, resources) => "{}");
        }

        [Fact]
        public async Task HandleAccountInfoQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new SearchAccountQuery(viewer);
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
            accountInfoService
                .GetUsernamesSuggestion(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(expectedViewModel);

            var actualViewModel = accountInfoHandler.Handle(message).GetAwaiter().GetResult();

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleDownlineListQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new DownlineListQuery(viewer)
            {
                SearchAccountId = 1,
                PageIndex = 1,
                RoleId = 3,
                StatusId = 1,
                TotalPages = 0
            };

            var downlineList = new List<DownlineDto>
            {
                new DownlineDto("Supertest1")
            };

            var totalDownlines = new List<TotalDownlinesSummary>
            {
                new TotalDownlinesSummary(2, 1, 0, 2)
            };

            var model = new DownlineListModel(downlineList, totalPages: 0);
            var expectedViewModel = new DownlineListViewModel(model, totalDownlines);

            accountInfoService.GetDownlineList(
                Arg.Any<int>(), 
                Arg.Any<int>(), 
                Arg.Any<byte>(),
                Arg.Any<byte>(),
                Arg.Any<int>()).Returns(model);

            accountInfoService
             .GetTotalDownlinesSummary(Arg.Any<int>(), Arg.Any<byte>())
             .Returns(totalDownlines);

            var actualViewModel = accountInfoHandler
                .Handle(message)
                .GetAwaiter()
                .GetResult();

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleDownlineListQuery_TotalPagesGreaterThanZero_NotCallGetTotalDownlinesSummaryService()
        {
            var message = new DownlineListQuery(viewer)
            {
                SearchAccountId = 1,
                PageIndex = 1,
                RoleId = 2,
                StatusId = 1,
                TotalPages = 2
            };

            accountInfoHandler.Handle(message).GetAwaiter().GetResult();

            accountInfoService.DidNotReceive().GetTotalDownlinesSummary(Arg.Any<int>(), Arg.Any<byte>());
        }

        [Fact]
        public void HandleCustomerInfoQuery_Always_CallGetCustomerInfoModel()
        {
            accountInfoHandler.Handle(customerInfoQuery);

            accountInfoService.Received().GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>());
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoIsNull_ReturnFalse()
        {
            CustomerInfoModel nullCustomerInfo = null;
            accountInfoService
                .GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>())
                .Returns(Task.FromResult(nullCustomerInfo));

            var actualViewModel = accountInfoHandler
                .Handle(customerInfoQuery)
                .GetAwaiter()
                .GetResult();

            Assert.Null(actualViewModel);
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoHasData_ReturnCorrectViewModel()
        {
            var memberBalanceInfoViewModel = AccountInfoHelper.BuildMemberBalanceInfoViewModel(customerInfoQuery, customerInfo);
            accountInfoService.GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>()).Returns(customerInfo);
            accountInfoService.GetBalanceInfo(Arg.Any<int>(), Arg.Any<byte>()).Returns(AccountInfoHelper.MemberBalanceInfo);
            var expectedViewModel = new CustomerInfoViewModel(
                customerInfoQuery,
                customerInfo,
                memberBalanceInfoViewModel);

            var actualViewModel = accountInfoHandler.Handle(customerInfoQuery).GetAwaiter().GetResult();

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoHasData_CallGetBalanceInfoModel()
        {
            accountInfoService.GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>()).Returns(customerInfo);

            accountInfoHandler.Handle(customerInfoQuery);

            accountInfoService.Received().GetBalanceInfo(Arg.Any<int>(), Arg.Any<byte>());
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

            var actualViewModel = accountInfoHandler.Handle(message).GetAwaiter().GetResult();

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

            var actualViewModel = accountInfoHandler.Handle(message).GetAwaiter().GetResult();

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public void HandleCustomerStatusCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerNonProductStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            var model = Result.CreateSuccessResult();
            accountInfoService.UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>()).Returns(model);

            var actualViewModel = accountInfoHandler.Handle(message).GetAwaiter().GetResult();

            AssertExtension.Equal(actualViewModel, model);
        }

        [Fact]
        public void HandleCustomerAllStatusQuery_Always_ReturnsCorrectViewModel()
        {
            var message = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "SearchAccountName",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "UplineName",
                UplineRoleId = 4
            };

            var model = fixture.Create<CustomerStatusModel>();
            accountInfoService.GetCustomerStatus(Arg.Any<int>()).Returns(Task.FromResult(model));
            var expectedViewModel = new CustomererStatusViewModel(message, model);

            var actualViewModel = accountInfoHandler
                .Handle(message)
                .GetAwaiter()
                .GetResult();

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