namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.ProfilePicture.ViewModels;
    using MediatR;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, ProfielPictureViewModel>
    {
        public ProfilePictureHandler()
        {
        }

        public async Task<ProfielPictureViewModel> Handle(ProfilePictureQuery message)
            => new ProfielPictureViewModel();
    }
}