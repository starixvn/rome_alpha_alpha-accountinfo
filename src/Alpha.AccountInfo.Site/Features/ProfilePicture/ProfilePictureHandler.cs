namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Services.AccountInfo;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>,
        IAsyncRequestHandler<ProfilePictureLibaryQuery, IEnumerable<byte[]>>
    {
        private readonly ProfilePictureServices profilePictureServices;

        public ProfilePictureHandler(ProfilePictureServices profilePictureServices)
        {
            this.profilePictureServices = profilePictureServices;
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
            => new ProfielPictureViewModel();

        public async Task<IEnumerable<byte[]>> Handle(ProfilePictureLibaryQuery message)
           => await profilePictureServices.GetProfilePictureLibrary().ConfigureAwait(false);
    }
}