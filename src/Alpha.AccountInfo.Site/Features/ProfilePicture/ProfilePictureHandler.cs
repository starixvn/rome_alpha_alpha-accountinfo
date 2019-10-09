namespace Alpha.AccountInfo.Site.Features.ProfilePicture
{
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using MediatR;
    using System.Threading.Tasks;

    public class ProfilePictureHandler :
        IAsyncRequestHandler<ProfilePictureQuery, string>
    {
        public ProfilePictureHandler()
        {
        }

        public async Task<string> Handle(ProfilePictureQuery message)
            => string.Empty;
    }
}