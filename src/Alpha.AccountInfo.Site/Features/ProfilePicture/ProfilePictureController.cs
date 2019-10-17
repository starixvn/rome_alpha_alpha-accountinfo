namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Controllers
{
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ProfilePictureController : AccountInfoBaseController
    {
        private const string ProfilePictureViewPath = "~/Features/ProfilePicture/Views/CreateProfilePicture.cshtml";
        private const string IndexViewPath = "~/Features/ProfilePicture/Views/Index.cshtml";

        public ProfilePictureController(IMediator mediator)
         : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProfilePictureQuery message)
        => await ViewAsync(message, IndexViewPath).ConfigureAwait(false);

        public async Task<ActionResult> CreateProfilePicture(ProfilePictureQuery message)
         => await ViewAsync(message, ProfilePictureViewPath).ConfigureAwait(false);
    }
}