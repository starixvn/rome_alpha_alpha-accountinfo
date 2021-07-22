namespace Alpha.AccountInfo.Site.Tests.Features.OpenDownlines
{
    using Alpha.AccountInfo.Features.OpenDownlines;
    using Alpha.AccountInfo.Features.OpenDownlines.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.OpenDownlines;
    using Alpha.AccountInfo.Site.Features.OpenDownlines.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.Products.Core.Services;
    using AutoFixture;
    using Extensions;
    using NSubstitute;
    using Shared.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class OpenDownlinesHandlerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly Fixture fixture = new Fixture();
        private readonly OpenDownlinesService openDownlinesService;
        private readonly SearchAccountService accountInfoService;
        private readonly OpenDownlinesHandler handler;

        public OpenDownlinesHandlerTest()
        {
            SimpleServiceLocator
            .Instance
            .Register<Func<string, string, string>>((microSiteName, virtualPath) => "MicroSiteUrl/")
            .Register<Func<string, ResourceManager, string>>((language, resources) => "{}")
            .Register<Func<int, string>>((productId) => $"Product Name of {productId}")
            .Register<Func<string, string>>(key => $"value of {key}")
            .Register<Func<string, string, string>>((microSiteName, virtualPath) => $"microSiteName/");

            openDownlinesService = Substitute.For<OpenDownlinesService>();
            accountInfoService = Substitute.For<SearchAccountService>();
            handler = new OpenDownlinesHandler(openDownlinesService, accountInfoService);
        }

        [Fact]
        public void HandleOpenSingleRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new OpenSingleRequest(viewer)
            {
                SearchAccountName = "test",
                SearchAccountId = 1
            };

            var model = fixture.Create<IEnumerable<OpenSingleModel>>();

            var expectedViewModel = new OpenSingleViewModel(message.Language,
                    new OpenSingleDataViewModel(
                        message.SearchAccountId,
                        message.SearchAccountName,
                        model));

            openDownlinesService
             .GetOpenInfoModel(Arg.Any<int>())
             .Returns(Task.FromResult(model));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleOpenViewListRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new OpenViewListRequest(viewer)
            {
                SearchAccountName = "test",
                SearchAccountId = 1,
                IsAffected = true,
                DownlineRoleId = 1,
                PageSize = 50,
                PageIndex = 1
            };

            var model = fixture.Create<OpenViewListModel>();

            var expectedViewModel = new OpenListViewModel(
                message.SearchAccountId,
                message.SearchAccountName,
                model);

            openDownlinesService
             .GetOpenViewListModel(Arg.Any<int>(), Arg.Any<bool>(), Arg.Any<byte>(), Arg.Any<int>(), Arg.Any<int>())
             .Returns(Task.FromResult(model));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleOpenMultipleRequest_Always_ReturnsCorrectViewModel()
        {
            var message = new OpenMultipleRequest(viewer)
            {
                CustIds = "1, 2",
                RoleId = 3
            };

            var model = fixture.Create<IEnumerable<OpenMultipleModel>>();

            var expectedViewModel = new OpenMultipleViewModel(message.RoleId, message.Language, model);

            openDownlinesService
             .GetOpenMultipleModel(Arg.Any<string>(), Arg.Any<byte>())
             .Returns(Task.FromResult(model));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void HandleOpenSingleCommand_Always_ReturnsCorrectViewModel()
        {
            var message = new OpenSingleCommand(viewer)
            {
                SearchAccountId = 1
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
        public void HandleOpenMultipleCommand_IsValidMultipleBelonging_ReturnsCorrectViewModel()
        {
            var openCustomerInfoList = fixture.Create<IEnumerable<OpenCustomerInfo>>();
            var message = new OpenMultipleCommand(viewer)
            {
                OpenList = openCustomerInfoList
            };

            accountInfoService.CheckMultipleCustomesBelonging(
                Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>()).Returns(true);

            var result = Result.CreateSuccessResult();
            accountInfoService
            .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
            .Returns(result);

            var expectedViewModel = new OpenMultipleResultViewModel(
                UserLevel.Super.Value,
                openCustomerInfoList.Select(
                customerInfo => new OpenResult(customerInfo.Username, customerInfo.IsClosed, result)));

            var actualViewModel =
                handler.Handle(message)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }

        [Fact]
        public void UpdateOpenAsync_Always_ReturnsCorrectViewModel()
        {
            var message = new OpenMultipleCommand(viewer)
            {
                OpenList = fixture.Create<IEnumerable<OpenCustomerInfo>>()
            };

            var customer = fixture.Create<OpenCustomerInfo>();

            var result = Result.CreateSuccessResult();

            var expectedViewModel = new OpenResult(customer.Username, customer.IsClosed, result);

            accountInfoService
             .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusParam>())
             .Returns(result);

            var actualViewModel =
                handler.UpdateCloseAsync(message, customer)
                .GetAwaiter()
                .GetResult(); ;

            AssertExtension.Equal(actualViewModel, expectedViewModel);
        }
    }
}