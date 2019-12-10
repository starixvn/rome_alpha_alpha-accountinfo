namespace Alpha.AccountInfo.Site.Tests.Features.SearchAccount
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Controllers;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Tests.Features.Shared.Helpers;
    using MediatR;
    using NSubstitute;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "ProfilePicture")]
    public class ProfilePictureControllerTest
    {
        private readonly IUser viewer = AccountInfoHelper.ViewerFixture(UserLevel.Super.Value);
        private readonly IMediator mediator;
        private readonly ProfilePictureController controller;

        public ProfilePictureControllerTest()
        {
            mediator = Substitute.For<IMediator>();
            controller = new ProfilePictureController(mediator);
        }

        [Fact]
        public async Task HandleProfilePictureQuery_Always_CallsMediatorSendQuery()
        {
            var message = new ProfilePictureQuery(viewer);

            await controller.Index(message);

            await mediator.Received().Send(message);
        }

        [Fact]
        public async Task HandleProfilePictureLibraryQuery_Always_CallsMediatorSendQuery()
        {
            var message = new ProfilePictureLibraryQuery(viewer);

            await controller.GetPictureLibrary(message);

            await mediator.Received().Send(message);
        }
        [Fact]
        public void UpdateActorProfilePictureQuery_ValidViewModel_ReturnResult()
        {
            var message = new UpdateActorProfilePictureRequest(viewer)
            {
                ImageId = 100
            };

            controller
                .UpdateActorProfilePicture(message)
                .GetAwaiter()
                .GetResult(); 

            mediator.Received().Send(message);
        }
    }
}