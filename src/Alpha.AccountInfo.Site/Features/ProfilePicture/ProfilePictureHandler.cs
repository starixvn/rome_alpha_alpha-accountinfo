namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.SearchAccount.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.ProfilePicture.Services;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
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
        private readonly ProfilePictureServices profilePictureServices;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
        {
            var actorAvatarBinary = await profilePictureServices.GetActorProfilePicture(message.ActorId).ConfigureAwait(false);
            var binaryImages = await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);
            var images = binaryImages.Select(avatar => new DefaultProfielPictureViewModel(
                   avatar.ImageId,
                   GenerateImage(avatar.Image))).Take(4);

            return new ProfielPictureViewModel(
                images, 
                new DefaultProfielPictureViewModel(0, GenerateImage(actorAvatarBinary.Image)));
        }

        public async Task<IEnumerable<DefaultProfielPictureViewModel>> Handle(ProfilePictureLibaryQuery message)
        {
            var binaryImages = await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);

            return binaryImages.Select(avatar => 
                new DefaultProfielPictureViewModel(
                   avatar.ImageId,
                   GenerateImage(avatar.Image)));
        }

        public Task<Result> Handle(UpdateActorProfilePictureQuery message)
          => profilePictureServices.UpdateActorProfilePicture(
              new UpdateActorProfilePictureParam(
                  message.ActorId,
                  message.ImageId,
                  message.Image,
                  message.IsDefault,
                  message.IP));

        private string GenerateImage(byte[] binaryImages)
            => binaryImages != null ?
            $"data:image/jpeg;base64,{Convert.ToBase64String(binaryImages)}" :
            string.Empty;
    }
}