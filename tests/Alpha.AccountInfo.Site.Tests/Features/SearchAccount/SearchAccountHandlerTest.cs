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
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Alpha.AccountInfo.Site.Handlers;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Alpha.AccountInfo.Tests.Utility;
    using Alpha.Products.AccountInfo;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting;
    using Alpha.Products.AccountInfo.Setting.ViewModels;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Products;
    using Alpha.Products.AccountInfo.Status;
    using Alpha.Products.AccountInfo.Status.ViewModels;
    using Alpha.Products.AccountInfo.Status.ViewModels.Base;
    using Alpha.Products.AccountInfo.Tests.Utility;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.Models;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using Alpha.Products.Core.ViewModels;
    using Alpha.SecurityCode.Services;
    using AutoFixture;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;
    using static Alpha.AccountInfo.Tests.Utility.AppProductConfigurationHelper;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly CustomerInfoModel customerInfo = AccountInfoHelper.BuildCustomerInfo();
        private readonly SearchAccountService accountInfoService;
        private readonly SecurityCodeChecker securityCodeChecker;
        private readonly SearchAccountHandler accountInfoHandler;
        private readonly Fixture fixture = new Fixture();
        private readonly CustomerInfoQuery customerInfoQuery;
        private readonly IDictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>> getBalanceInfoViewModelByLevel;
        private readonly CompareLogic compareLogic = new CompareLogic();
        private readonly Customer actor = new Customer(1, 0, "username1", 4, CustomerHelper.CreateCustomerProductStatus());
        private readonly Customer searchAccount = new Customer(2, 0, "username2", 3, CustomerHelper.CreateCustomerProductStatus());
        private readonly ISearchAccountInfoRequest request = RequestHelper.CreateRequest(4, 3);

        public SearchAccountHandlerTest()
        {
            var configurationLoader = Substitute.For<IConfigurationLoader>();
            configurationLoader.Load().Returns(LoadApplicationProductConfiguration());

            SimpleServiceLocator
             .Instance
             .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
             .Register<Func<string, ResourceManager, string>>((language, resources) => "{}")
             .Register<IConfigurationLoader>(configurationLoader)
             .Register<GenericViewModelFactory<ISearchAccountInfoRequest, IStatusItemViewModel>>(new StatusViewModelFactory())
             .Register<GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>>(new SettingViewModelFactory())
             .Register<Func<int, string>>((productId) => $"Product Name of {productId}")
             .Register<Func<string, string>>(key => $"value of {key}")
             .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            getBalanceInfoViewModelByLevel =
               new Dictionary<UserLevel, Func<CustomerInfoQuery, CustomerInfoModel, IBalanceInfoModel, IBalanceInfoViewModel>>
               {
                    {UserLevel.Master, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Agent, BuildMemberBalanceInfoViewModel},
                    {UserLevel.Member, BuildMemberBalanceInfoViewModel}
               };

            accountInfoService = Substitute.For<SearchAccountService>();
            securityCodeChecker = Substitute.For<SecurityCodeChecker>();
            securityCodeChecker.IsEnableSecurityCode().Returns(true);
            accountInfoHandler = new SearchAccountHandler(accountInfoService, getBalanceInfoViewModelByLevel, null, null, securityCodeChecker);

            customerInfoQuery = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = "test"
            };
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
            accountInfoHandler.Handle(customerInfoQuery).ConfigureAwait(false);

            accountInfoService.Received().GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());
        }

        [Fact]
        public void HandleCustomerInfoQuery_CustomerInfoIsNull_ReturnFalse()
        {
            CustomerInfoModel nullCustomerInfo = null;
            accountInfoService
                .GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
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
            accountInfoService.GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(customerInfo);
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
            accountInfoService.GetCustomerInfo(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(customerInfo);

            accountInfoHandler.Handle(customerInfoQuery).ConfigureAwait(false);

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

        ////[Fact]
        ////public void HandleCustomerAllStatusQuery_Always_ReturnsCorrectViewModel()
        ////{
        ////    var message = new CustomerStatusQuery(viewer)
        ////    {
        ////        SearchAccountId = 1,
        ////        SearchAccountName = "SearchAccountName",
        ////        SearchAccountRoleId = 3,
        ////        UplineId = 2,
        ////        UplineName = "UplineName",
        ////        UplineRoleId = 4
        ////    };

        ////    var model = fixture.Create<CustomerStatusModel>();
        ////    accountInfoService.GetCustomerStatus(Arg.Any<int>()).Returns(Task.FromResult(model));
        ////    var expectedViewModel = new CustomerStatusViewModel(message, model);

        ////    var actualViewModel = accountInfoHandler
        ////        .Handle(message)
        ////        .GetAwaiter()
        ////        .GetResult();

        ////    Assert.True(compareLogic.Compare(actualViewModel, expectedViewModel).AreEqual);
        ////}

        [Fact]
        public void BuildCustomerProductSettings_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var expectedSetting = new List<ISettingItemViewModel>
            {
                new BetSettingViewModel(request),
                new CommissionViewModel(request),
                new EditInfoViewModel(request),
                new RemoveOtpViewModel(request),
                new ResetPasswordViewModel(request),
                new ResetSecurityCodeViewModel(request),
                new SportsbookMinPTViewModel(request),
                new TransferViewModel(request),
                new SportsbookViewModel(request)
            }
            .Where(setting => setting.Visible)
            .OrderBy(setting => setting.DisplayOrder);

            var actualSettings = accountInfoHandler.BuildCustomerProductSettings(actor, searchAccount, message, CreateCustomerStatusModelStub());
            Assert.True(compareLogic.Compare(actualSettings, expectedSetting).AreEqual);
        }

        [Fact]
        public void BuildCustomerProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var configItem = Substitute.For<IConfigurationItem>();
            configItem.ProductId.Returns(1);

            var expectedStatus = new List<IStatusItemViewModel>
            {
                new ConfirmAllowAutoMasterPTViewModel(request),
                new ConfirmAllowOutrightViewModel(request),
                new ConfirmClosedViewModel(request),
                new ConfirmDoubleCommViewModel(request),
                new ConfirmSuspendedViewModel(request),
                new  ProductStatusItemViewModel(request, configItem)
            }
            .Where(status => status.Visible)
            .OrderBy(status => status.DisplayOrder);

            var actualStatus = accountInfoHandler.BuildCustomerProductStatus(actor, searchAccount, message, CreateCustomerStatusModelStub());
            Assert.True(compareLogic.Compare(actualStatus, expectedStatus).AreEqual);
        }

        [Fact]
        public void BuildActorProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var message = new CustomerStatusQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "username1",
                SearchAccountRoleId = 3,
                UplineId = 2,
                UplineName = "username2",
                UplineRoleId = 4
            };

            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.ColossusBets, true, hidden: true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualActorProductStatus = accountInfoHandler.BuildActorProductStatus(message, Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualActorProductStatus, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildSearchAccountProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.ColossusBets, true, hidden: true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualSearchAccountProduct = accountInfoHandler.BuildSearchAccountProductStatus(StubNonDCSProduct(), Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualSearchAccountProduct, customerProductStatus).AreEqual);
        }

        [Fact]
        public void BuildUplineProductStatus_Always_ReturnsCorrectSettingViewModel()
        {
            var customerProductStatus = new List<CustomerProductStatus>
            {
                new CustomerProductStatus(ProductConstants.Sportsbook, true),
                new CustomerProductStatus(ProductConstants.NumberGame, true),
                new CustomerProductStatus(ProductConstants.RNGCasino, true, synced: true),
                new CustomerProductStatus(ProductConstants.ESport, true),
                new CustomerProductStatus(ProductConstants.ColossusBets, true),
                new CustomerProductStatus(ProductConstants.Keno, hidden: true),
                new CustomerProductStatus(ProductConstants.GoldDeluxe, true, true)
            };

            var actualUplineProductStatus = accountInfoHandler.BuildUplineProductStatus(StubNonDCSProduct(), Enumerable.Empty<DCSProductStatusModel>());
            Assert.True(compareLogic.Compare(actualUplineProductStatus, customerProductStatus).AreEqual);
        }

        private static CustomerStatusModel CreateCustomerStatusModelStub()
            => new CustomerStatusModel(StubNonProductStatus(), StubNonDCSProduct(), StubDcsProducts());

        private static NonDCSProductStatusModel StubNonDCSProduct()
            => new NonDCSProductStatusModel(
                       syncCasino: true,
                       disableCasino: true,
                       uplineDisableCS: true,
                       disableNumber: true,
                       uplineDisableNumber: true,
                       disableEsports: true,
                       uplineDisableEsports: true,
                       disableColossusBet: true,
                       hideColossusBet: true,
                       uplineDisableColossusBet: true,
                       hideKeno: true,
                       uplineHideGoldDeluxe: true,
                       uplineDisableGoldDeluxe: true,
                       disableGoldDeluxe: true,
                       hideGoldDeluxe: true);

        private static NonProductStatusModel StubNonProductStatus()
            => new NonProductStatusModel(
                             hasDownline: true,
                             disableAutoMPT: true,
                             closed: true,
                             uplineClosed: true,
                             suspended: true,
                             isDirectSuspend: true,
                             uplineSuspend: true,
                             transferOption: "00000000",
                             doubleCommLosing: true,
                             uplineDoubleCommLosing: true,
                             disableOutright: true,
                             uplineDisableOutright: true);

        private static List<DCSProductStatusModel> StubDcsProducts()
            => new List<DCSProductStatusModel>
                 {
                     new DCSProductStatusModel(
                                  productId: 5,
                                  sportType: 12,
                                  betType: 24,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true),
                     new DCSProductStatusModel(
                                  productId: 6,
                                  sportType: 13,
                                  betType: 26,
                                  isSync: true,
                                  isHidden: true,
                                  isDisabled: true,
                                  isSyncedUpline: true,
                                  isHiddenUpline: true,
                                  isDisabledUpline: true,
                                  hasDownline: true)
                    };

        private IBalanceInfoViewModel BuildMemberBalanceInfoViewModel(
           CustomerInfoQuery message,
           CustomerInfoModel customerInfo,
           IBalanceInfoModel balanceInfoModel)
            => AccountInfoHelper.BuildMemberBalanceInfoViewModel(
               customerInfoQuery,
               customerInfo);
    }
}