namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Features.ProfilePicture.Models;
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibaryQuery, IEnumerable<string>>
    {
        private readonly ProfilePictureServices profilePictureServices;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
            => new ProfielPictureViewModel();

        public async Task<IEnumerable<string>> Handle(ProfilePictureLibaryQuery message)
        {
            var binaryImages = await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);

            return binaryImages.Select(avatar => $"data:image/jpeg;base64,{Convert.ToBase64String(avatar.Image)}");
        }
    }
}