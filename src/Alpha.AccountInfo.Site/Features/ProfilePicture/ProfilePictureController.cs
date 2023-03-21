namespace Alpha.AccountInfo.Site.Features.ProfilePicture.Controllers
{
    using Alpha.AccountInfo.Site.Features.ProfilePicture.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class ProfilePictureController : AccountInfoBaseController
    {
        private const string IndexViewPath = "~/Features/ProfilePicture/Views/Index.cshtml";

        public ProfilePictureController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProfilePictureQuery message)
            => await ViewAsync(message, IndexViewPath).ConfigureAwait(false);

        [HttpPost]
        public async Task<ActionResult> GetPictureLibrary(ProfilePictureLibraryQuery message)
            => await JsonDataAsync(message, JsonRequestBehavior.DenyGet).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateActorProfilePicture(UpdateActorProfilePictureRequest query)
            => await JsonDataAsync(query, JsonRequestBehavior.DenyGet).ConfigureAwait(false);
    }
}