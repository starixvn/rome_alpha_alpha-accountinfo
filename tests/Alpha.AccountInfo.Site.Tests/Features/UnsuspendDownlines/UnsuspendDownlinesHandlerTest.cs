namespace Alpha.AccountInfo.Site.Tests.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using Alpha.AccountInfo.Site.Tests.Extensions;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using Alpha.Products.Core.Services;
    using AutoFixture;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class UnsuspendDownlinesHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly Fixture fixture = new Fixture();
        private readonly UnsuspendDownlinesService unsuspendDownlinesService;
        private readonly SearchAccountService accountInfoService;
        private readonly UnsuspendDownlinesHandler handler;

        public UnsuspendDownlinesHandlerTest()
        {
            SimpleServiceLocator
            .Instance
            .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
            .Register<Func<string, ResourceManager, string>>((language, resources) => "{}")
            .Register<Func<int, string>>((productId) => $"Product Name of {productId}")
            .Register<Func<string, string>>(key => $"value of {key}")
            .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            unsuspendDownlinesService = Substitute.For<UnsuspendDownlinesService>();
            accountInfoService = Substitute.For<SearchAccountService>();
            handler = new UnsuspendDownlinesHandler(unsuspendDownlinesService, accountInfoService);
        }

        [Fact]
        public void HandleUnsuspendSingleRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new UnsuspendSingleRequest(viewer)
            {
                SearchAccountName = "test",
                SearchAccountId = 1
            };

            var model = fixture.Create<IEnumerable<UnsuspendSingleModel>>();

            var expectedViewModel = new UnsuspendSingleViewModel( message.Language,
                    new UnsuspendSingleDataViewModel(
                        message.SearchAccountId, 
                        message.SearchAccountName, 
                        model));

            unsuspendDownlinesService
             .GetUnsuspendInfoModel(Arg.Any<int>())
             .Returns(Task.FromResult(model));

            var actualViewModel = 
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUnsuspendViewListRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new UnsuspendViewListRequest(viewer)
            {
                SearchAccountName = "test",
                SearchAccountId = 1,
                IsAffected = true,
                DownlineRoleId = 1,
                PageSize = 50,
                PageIndex = 1
            };

            var model = fixture.Create<UnsuspendViewListModel>();

            var expectedViewModel = new UnsuspendListViewModel(
                message.SearchAccountId,
                message.SearchAccountName,
                model);

            unsuspendDownlinesService
             .GetUnsuspendViewListModel(Arg.Any<int>(), Arg.Any<bool>(), Arg.Any<byte>(), Arg.Any<int>(), Arg.Any<int>())
             .Returns(Task.FromResult(model));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUnsuspendMultipleRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new UnsuspendMultipleRequest(viewer)
            {
                CustIds = "1, 2",
                RoleId = 3
            };

            var model = fixture.Create<IEnumerable<UnsuspendMultipleModel>>();

            var expectedViewModel = new UnsuspendMultipleViewModel(message.RoleId, message.Language, model);

            unsuspendDownlinesService
             .GetUnsuspendMultipleModel(Arg.Any<string>(), Arg.Any<byte>())
             .Returns(Task.FromResult(model));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUnsuspendSingleCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new UnsuspendSingleCommand(viewer)
            {
                SearchAccountId =  1
            };

            var result = Result.CreateSuccessResult();

            var expectedViewModel = result;

            accountInfoService
             .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
             .Returns(result);

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleUnsuspendMultipleCommand_IsValidMultipleBelonging_ReturnsCorrectViewModel()
        {
            var unsuspendCustomerInfoList = fixture.Create<IEnumerable<UnsuspendCustomerInfo>>();
            var message = new UnsuspendMultipleCommand(viewer)
            {
                UnsuspendList = unsuspendCustomerInfoList
            };

            unsuspendDownlinesService.CheckMultipleCustomesBelonging(
                Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(true);

            var result = Result.CreateSuccessResult();
            accountInfoService
            .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
            .Returns(result);

            var expectedViewModel = new UnsuspendMultipleResultViewModel(
                unsuspendCustomerInfoList.Select(
                customerInfo => new UnsuspendResult(customerInfo.Username, customerInfo.Suspended, result)));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void UpdateSuspendAsync_Always_ReturnsCorrectViewModel()
        {
            var message = new UnsuspendMultipleCommand(viewer)
            {
                UnsuspendList = fixture.Create<IEnumerable<UnsuspendCustomerInfo>>()
            };

            var customer = fixture.Create<UnsuspendCustomerInfo>();

            var result = Result.CreateSuccessResult();

            var expectedViewModel = new UnsuspendResult(customer.Username, customer.Suspended, result);

            accountInfoService
             .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
             .Returns(result);

            var actualViewModel = 
                handler.UpdateSuspendAsync(message, customer)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }
    }
}