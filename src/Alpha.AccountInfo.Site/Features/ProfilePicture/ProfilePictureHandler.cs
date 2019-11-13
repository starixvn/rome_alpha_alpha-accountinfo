namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared.Helpers;
    using Fanex.Logging.Extensions.Web;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibraryQuery, IEnumerable<ProfilePictureModel>>,
        IAsyncRequestHandler<UpdateActorProfilePictureQuery, Result>
    {
        private const int NumerousOfDefaultPictures = 6;
        private readonly ProfilePictureServices profilePictureServices;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
        {
            try
            {
                var actorAvatar = await profilePictureServices.GetActorProfilePicture(message.ActorId).ConfigureAwait(false);
                var imageLibrary = await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);

                return new ProfielPictureViewModel(
                    imageLibrary.OrderBy(n => Guid.NewGuid()).Take(NumerousOfDefaultPictures).Concat(new[] { actorAvatar }),
                    message.Language);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return new ProfielPictureViewModel(Enumerable.Empty<ProfilePictureModel>(), message.Language);
            }
        }

        public async Task<IEnumerable<ProfilePictureModel>> Handle(ProfilePictureLibraryQuery message)
            => await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);

        public Task<Result> Handle(UpdateActorProfilePictureQuery message)
          => profilePictureServices.UpdateActorProfilePicture(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.ImageBinary,
                  message.IsDefault,
                  message.IP));
    }
}