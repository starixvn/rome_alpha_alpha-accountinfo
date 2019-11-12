namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using Fanex.Caching;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibaryQuery, IEnumerable<DefaultProfielPictureViewModel>>,
        IAsyncRequestHandler<UpdateActorProfilePictureQuery, Result>
    {
        private const string ProfilePictureLibaryKey = "Alpha.AccountInfo.Site.Features.ProfilePicture.Library";
        private const int NumerousOfDefaultPictures = 6;
        private readonly ProfilePictureServices profilePictureServices;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
        {
            var actorAvatarBinary = await profilePictureServices.GetActorProfilePicture(message.ActorId).ConfigureAwait(false);
            var binaryImages = await GetProfilePictureLibrary().ConfigureAwait(false);

            var images = binaryImages.OrderBy(n => Guid.NewGuid()).Take(NumerousOfDefaultPictures).Concat(
                actorAvatarBinary == null ? 
                Enumerable.Empty<DefaultProfielPictureViewModel>() :
                new[] { new DefaultProfielPictureViewModel(0, GenerateImage(actorAvatarBinary?.Image)) });

            return new ProfielPictureViewModel(images, message.Language);
        }

        public async Task<IEnumerable<DefaultProfielPictureViewModel>> Handle(ProfilePictureLibaryQuery message)
            => await GetProfilePictureLibrary().ConfigureAwait(false);

        public Task<Result> Handle(UpdateActorProfilePictureQuery message)
          => profilePictureServices.UpdateActorProfilePicture(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.ImageBinary,
                  message.IsDefault,
                  message.IP));

        internal async Task<IEnumerable<DefaultProfielPictureViewModel>> GetProfilePictureLibrary()
            => await await ConfiguredCacheService.GetOrSetAsync(
                    ProfilePictureLibaryKey,
                    async () => await GetProfilePictureLibraryFromService().ConfigureAwait(false)).ConfigureAwait(false);

        internal async Task<IEnumerable<DefaultProfielPictureViewModel>> GetProfilePictureLibraryFromService()
        {
            var binaryImages = await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);

           return binaryImages.Select(avatar =>
                new DefaultProfielPictureViewModel(
                   avatar.ImageId,
                   GenerateImage(avatar.Image)));
        }

        private string GenerateImage(byte[] binaryImages)
            => binaryImages != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(binaryImages)}" :
            string.Empty;
    }
}