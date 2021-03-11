namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using MediatR;
    using NSubstitute;
    using Site.Controllers;
    using Xunit;

    [Trait("Category", "AccountInfo")]
    public class SearchAccountControllerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Agent.Value);
        private readonly IMediator mediator;
        private readonly SearchAccountController accountInfoController;

        public SearchAccountControllerTest()
        {
            mediator = Substitute.For<IMediator>();
            accountInfoController = new SearchAccountController(mediator);
        }

        [Fact]
        public async Task HandleAccountInfoQuery_Always_CallsMediatorSendAccountInfoQuery()
        {
            var message = new SearchAccountQuery(viewer);

            await accountInfoController.Index(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public void CustomerInfoQuery_ValidViewModel_ReturnsView()
        {
            var message = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = "test"
            };

            accountInfoController.GetCustomerInfo(message).GetAwaiter().GetResult();

            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CustomerInfoQuery_InvalidUserName_ReturnsInvalidPageView(string userName)
        {
            var message = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = userName
            };

            var actualResult = accountInfoController.GetCustomerInfo(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        [Fact]
        public void CustomerInfoQuery_UserName_Compare_ActorName_ReturnsInvalidPageView()
        {
            var message = new CustomerInfoQuery(viewer)
            {
                SearchAccountName = "ActorName"
            };

            var actualResult = accountInfoController.GetCustomerInfo(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        [Fact]
        public void DownlineListQuery_ValidViewModel_ReturnsView()
        {
            var message = new DownlineListQuery(viewer)
            {
                SearchAccountId = 2
            };

            accountInfoController.GetDownlineList(message).GetAwaiter().GetResult();

            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void DownlineListQuery_InvalidUserName_ReturnsInvalidPageView(int searchAccountId)
        {
            var message = new DownlineListQuery(viewer)
            {
                SearchAccountId = searchAccountId
            };

            var actualResult = accountInfoController.GetDownlineList(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        [Fact]
        public void OnlineListQuery_ValidViewModel_ReturnsView()
        {
            var message = new OnlineListQuery(viewer)
            {
                SearchAccountId = 2
            };

            accountInfoController.GetOnlineList(message).GetAwaiter().GetResult();

            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void OnlineListQuery_InvalidUserName_ReturnsInvalidPageView(int searchAccountId)
        {
            var message = new OnlineListQuery(viewer)
            {
                SearchAccountId = searchAccountId
            };

            var actualResult = accountInfoController.GetOnlineList(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        //[Fact]
        //public void CustomerAllStatusQuery_ValidViewModel_ReturnsView()
        //{
        //    var message = new CustomerStatusQuery(viewer)
        //    {
        //        SearchAccountId = 2,
        //        SearchAccountName = "SearchAccountName",
        //        SearchAccountRoleId = 1,
        //        UplineId = 2,
        //        UplineName = "UplineName",
        //        UplineRoleId = 4
        //    };

        //    accountInfoController.GetCustomerAllStatus(message).GetAwaiter().GetResult();

        //    mediator.Received().Send(message);
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(-1)]
        //public void CustomerAllStatusQuery_InvalidUserName_ReturnsInvalidPageView(int searchAccountId)
        //{
        //    var message = new CustomerStatusQuery(viewer)
        //    {
        //        SearchAccountId = searchAccountId,
        //        SearchAccountName = "SearchAccountName",
        //        SearchAccountRoleId = 3,
        //        UplineId = 2,
        //        UplineName = "UplineName",
        //        UplineRoleId = 4
        //    };

        //    var actualResult = accountInfoController.GetCustomerAllStatus(message).GetAwaiter().GetResult() as JsonResult;
        //    var actualData = (Result)actualResult.Data;

        //    Assert.True(!actualData.IsSuccess);
        //}

        [Fact]
        public async Task HandleGivenCreditQuery_Always_CallsMediatorSendAccountInfoQuery()
        {
            var message = new GivenCreditQuery(viewer)
            {
                SearchAccountId = 2,
                SearchAccountName = "test",
                SearchAccountRoleId = 1
            };

            await accountInfoController.GivenCredit(message);

            await mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task HandleGivenCreditQuery_InvalidSearchAccountId_ReturnsInvalidPageView(int searchAccountId)
        {
            var message = new GivenCreditQuery(viewer)
            {
                SearchAccountId = searchAccountId,
                SearchAccountName = "test",
                SearchAccountRoleId = 2
            };

            // Act
            var actualView = await accountInfoController.GivenCredit(message) as ViewResult;

            // Assert
            Assert.Equal("~/Features/Shared/Views/NoInformation.cshtml", actualView.ViewName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task HandleGivenCreditQuery_InvalidSearchAccountName_ReturnsInvalidPageView(string searchAccountName)
        {
            var message = new GivenCreditQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = searchAccountName,
                SearchAccountRoleId = 2
            };

            var actualView = await accountInfoController.GivenCredit(message) as ViewResult;

            Assert.Equal("~/Features/Shared/Views/NoInformation.cshtml", actualView.ViewName);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(0)]
        public async Task HandleGivenCreditQuery_InvalidSearchAccountRoleId_ReturnsInvalidPageView(byte SearchAccountRoleId)
        {
            var message = new GivenCreditQuery(viewer)
            {
                SearchAccountId = 1,
                SearchAccountName = "test",
                SearchAccountRoleId = SearchAccountRoleId
            };

            var actualView = await accountInfoController.GivenCredit(message) as ViewResult;

            Assert.Equal("~/Features/Shared/Views/NoInformation.cshtml", actualView.ViewName);
        }

        [Fact]
        public void GivenCreditCommand_ValidViewModel_ReturnsView()
        {
            var message = new GivenCreditCommand(viewer)
            {
                SearchAccountId = 2,
                CreditAmount = 1000,
                SearchAccountRoleId = 1
            };

            accountInfoController.UpdateGivenCredit(message).GetAwaiter().GetResult();

            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenCreditCommand_InvalidSearchAccountId_ReturnsInvalidPageView(int searchAccountId)
        {
            var message = new GivenCreditCommand(viewer)
            {
                SearchAccountId = searchAccountId,
                CreditAmount = 1000,
                SearchAccountRoleId = 1
            };

            var actualResult = accountInfoController.UpdateGivenCredit(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(0)]
        public void GivenCreditCommand_InvalidSearchAccountRoleId_ReturnsInvalidPageView(byte SearchAccountRoleId)
        {
            var message = new GivenCreditCommand(viewer)
            {
                SearchAccountId = 1,
                CreditAmount = 1000,
                SearchAccountRoleId = SearchAccountRoleId
            };

            var actualResult = accountInfoController.UpdateGivenCredit(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }

        [Fact]
        public void CustomerStatusCommand_ValidViewModel_ReturnsView()
        {
            var message = new CustomerNonProductStatusCommand(viewer)
            {
                SearchAccountId = 2,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            accountInfoController.UpdateCustomerStatus(message).GetAwaiter().GetResult();

            mediator.Received().Send(message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CustomerStatusCommand_InvalidSearchAccountId_ReturnsInvalidPageView(int searchAccountId)
        {
            var message = new CustomerNonProductStatusCommand(viewer)
            {
                SearchAccountId = searchAccountId,
                SyncedCasino3rd = true,
                StatusName = "outright",
                StatusValue = true
            };

            var actualResult = accountInfoController.UpdateCustomerStatus(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }
        [Fact]
        public void CustomerStatusCommand_InvalidStatusName_ReturnsInvalidPageView()
        {
            var message = new CustomerNonProductStatusCommand(viewer)
            {
                SearchAccountId = 1,
                SyncedCasino3rd = true,
                StatusName = string.Empty,
                StatusValue = true
            };

            var actualResult = accountInfoController.UpdateCustomerStatus(message).GetAwaiter().GetResult() as JsonResult;
            var actualData = (Result)actualResult.Data;

            Assert.True(!actualData.IsSuccess);
        }
    }
}