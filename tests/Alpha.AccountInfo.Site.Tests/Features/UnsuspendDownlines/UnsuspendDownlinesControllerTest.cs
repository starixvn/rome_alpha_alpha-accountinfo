namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using AutoFixture;
    using MediatR;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "UnsuspendDownlines")]
    public class UnsuspendDownlinesControllerTest
    {
        private readonly Fixture fixture = new Fixture();
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
        private readonly IMediator mediator;
        private readonly UnsuspendDownlinesController controller;

        public UnsuspendDownlinesControllerTest()
        {
            mediator = Substitute.For<IMediator>();
            controller = new UnsuspendDownlinesController(mediator);
        }

        [Fact]
        public async Task HandleUnsuspendSingleRequest_Always_CallsMediatorSendQuery()
        {
            var message = new UnsuspendSingleRequest(viewer)
            {
                SearchAccountName = "test1",
                SearchAccountId = 22
            };

            await controller.EditSingle(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public async Task HandleUnsuspendMultipleRequest_Always_CallsMediatorSendQuery()
        {
            var message = new UnsuspendMultipleRequest(viewer)
            {
                CustIds = "1, 2",
                RoleId = 1
            };

            await controller.EditMultiple(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public void HandleUnsuspendViewListRequest_Always_CallsMediatorSendQuery()
        {
            var message = new UnsuspendViewListRequest(viewer)
            {
                SearchAccountName = "test",
                SearchAccountId = 333,
                IsAffected = true,
                DownlineRoleId = 2,
                PageSize = 50,
                PageIndex = 1
            };

             controller
                .FetchCustomerList(message)
                .GetAwaiter()
                .GetResult();

            mediator.Received().Send(message);
        }

        [Fact]
        public void UnsuspendSingle_ValidViewModel_ReturnResult()
        {
            var message = new UnsuspendSingleCommand(viewer)
            {
                SearchAccountId = 1000
            };

            controller
                .UnsuspendSingle(message)
                .GetAwaiter()
                .GetResult(); 

            mediator.Received().Send(message);
        }

        [Fact]
        public void UnsuspendMultiple_ValidViewModel_ReturnResult()
        {
            var unsuspendCustomerInfoList = fixture.Create<IEnumerable<UnsuspendCustomerInfo>>();
            var message = new UnsuspendMultipleCommand(viewer)
            {
                UnsuspendList = unsuspendCustomerInfoList
            };

            controller
                .UnsuspendMultiple(message)
                .GetAwaiter()
                .GetResult(); 

            mediator.Received().Send(message);
        }
    }
}