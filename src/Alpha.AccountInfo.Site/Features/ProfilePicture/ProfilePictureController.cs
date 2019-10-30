namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Controllers
{
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ProfilePictureController : AccountInfoBaseController
    {
        private const string IndexViewPath = "~/Features/ProfilePicture/Views/Index.cshtml";
        private const string ProfilePictureViewPath = "~/Features/ProfilePicture/Views/CreateProfilePicture.cshtml";
        private const string PictureLibraryViewPath = "~/Features/ProfilePicture/Views/PictureLibrary.cshtml";

        public ProfilePictureController(IMediator mediator)
         : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProfilePictureQuery message)
            => await ViewAsync(message, IndexViewPath).ConfigureAwait(false);

        public async Task<ActionResult> CreateProfilePicture(ProfilePictureQuery message)
          => await ViewAsync(message, ProfilePictureViewPath).ConfigureAwait(false);

        //[HttpPost]
        //public async Task<ActionResult> CreateProfilePicture(CreateProfilePictureQuery message)
        //{
        //    return RedirectToAction("RedirectCreateProfilePictureIndex", "ProfilePicture");
        //}

        //public async Task<ActionResult> RedirectCreateProfilePictureIndex(CreateProfilePictureQuery message)
        //  => await ViewAsync(message, ProfilePictureViewPath).ConfigureAwait(false);

        public async Task<ActionResult> ViewPictureLibrary(ProfilePictureLibaryQuery message)
            => await ViewAsync(message, PictureLibraryViewPath).ConfigureAwait(false);

        //, AntiXsrfFilter
        [HttpPost]
        public async Task<ActionResult> UpdateActorProfilePicture(UpdateActorProfilePictureQuery query)
        => await JsonDataAsync(query, JsonRequestBehavior.DenyGet).ConfigureAwait(false);
    }
}