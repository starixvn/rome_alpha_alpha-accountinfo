namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Alpha.Products.Core.Services;
    using Alpha.ProfilePicture.Models;
    using Alpha.ProfilePicture.Params;
    using Alpha.ProfilePicture.Services;
    using Fanex.Caching;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibraryQuery, IEnumerable<ProfilePictureModel>>,
        IAsyncRequestHandler<UpdateActorProfilePictureQuery, ProfilePictureResult>
    {
        private const string ProfilePictureLibaryKey = "Alpha.AccountInfo.Site.Features.ProfilePicture.Library";
        private const int NumerousOfDefaultPictures = 7;
        private readonly ProfilePictureServices profilePictureServices;
        private readonly Func<int> getInsertProfilePictureSiteMode;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
            : this(profilePictureServices, null)
        {
        }

        internal ProfilePictureHandler(
            ProfilePictureServices profilePictureServices,
            Func<int> getInsertProfilePictureSiteMode)
        {
            this.profilePictureServices = profilePictureServices;
            this.getInsertProfilePictureSiteMode = getInsertProfilePictureSiteMode ??
             SimpleServiceLocator.GetService<Func<int>>();
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
        {
            var actorAvatar = await profilePictureServices.GetActorProfilePictureAsync(message.ActorId).ConfigureAwait(false);
            var imageLibrary = await GetProfilePictureLibraryAsync().ConfigureAwait(false);

            return new ProfielPictureViewModel(
                imageLibrary.OrderBy(n => Guid.NewGuid()).Take(NumerousOfDefaultPictures)
                .Concat(new[] { new ProfilePictureModel(actorAvatar.ImageId, actorAvatar.Image) }).Distinct(),
                message.Language,
                message.AllowUpdate);
        }

        public async Task<IEnumerable<ProfilePictureModel>> Handle(ProfilePictureLibraryQuery message)
            => await GetProfilePictureLibraryAsync().ConfigureAwait(false);

        public Task<ProfilePictureResult> Handle(UpdateActorProfilePictureQuery message)
          => profilePictureServices.UpdateActorProfilePictureAsync(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.ImageBinary,
                  getInsertProfilePictureSiteMode(),
                  false,
                  message.IP));

        public async Task<IEnumerable<ProfilePictureModel>> GetProfilePictureLibraryAsync()
            => await await ConfiguredCacheService.GetOrSetAsync(
                    ProfilePictureLibaryKey,
                    async () => await profilePictureServices.FectchProfilePictureLibraryAsync().ConfigureAwait(false)).ConfigureAwait(false);
    }
}