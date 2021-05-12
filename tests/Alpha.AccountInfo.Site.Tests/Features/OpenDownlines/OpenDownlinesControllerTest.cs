namespace Alpha.AccountInfo.Site.Tests.Features.OpenDownlines
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.OpenDownlines;
    using Alpha.AccountInfo.Site.Features.OpenDownlines.Requests;
    using AutoFixture;
    using MediatR;
    using NSubstitute;
    using Shared.Helpers;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "OpenDownlines")]
    public class OpenDownlinesControllerTest
    {
        private readonly Fixture fixture = new Fixture();
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
        private readonly IMediator mediator;
        private readonly OpenDownlinesController controller;

        public OpenDownlinesControllerTest()
        {
            mediator = Substitute.For<IMediator>();
            controller = new OpenDownlinesController(mediator);
        }

        [Fact]
        public async Task HandleOpenSingleRequest_Always_CallsMediatorSendQuery()
        {
            var message = new OpenSingleRequest(viewer)
            {
                SearchAccountName = "test1",
                SearchAccountId = 22
            };

            await controller.EditSingle(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public async Task HandleOpenMultipleRequest_Always_CallsMediatorSendQuery()
        {
            var message = new OpenMultipleRequest(viewer)
            {
                CustIds = "1, 2",
                RoleId = 1
            };

            await controller.EditMultiple(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public void HandleOpenViewListRequest_Always_CallsMediatorSendQuery()
        {
            var message = new OpenViewListRequest(viewer)
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
        public void OpenSingle_ValidViewModel_ReturnResult()
        {
            var message = new OpenSingleCommand(viewer)
            {
                SearchAccountId = 1000
            };

            controller
                .OpenSingle(message)
                .GetAwaiter()
                .GetResult();

            mediator.Received().Send(message);
        }

        [Fact]
        public void OpenMultiple_ValidViewModel_ReturnResult()
        {
            var openCustomerInfoList = fixture.Create<IEnumerable<OpenCustomerInfo>>();
            var message = new OpenMultipleCommand(viewer)
            {
                OpenList = openCustomerInfoList
            };

            controller
                .OpenMultiple(message)
                .GetAwaiter()
                .GetResult();

            mediator.Received().Send(message);
        }
    }
}