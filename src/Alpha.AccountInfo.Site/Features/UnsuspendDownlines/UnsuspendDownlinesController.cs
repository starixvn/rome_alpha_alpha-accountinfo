namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class UnsuspendDownlinesController : AccountInfoBaseController
    {
        private const string EditSinglePath = "~/Features/UnsuspendDownlines/Views/EditSingle.cshtml";
        private const string ViewUnsuspendListPath = "~/Features/UnsuspendDownlines/Views/ViewUnsuspendList.cshtml";
        public UnsuspendDownlinesController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> EditSingle(UnsuspendRequest message)
         => await ViewAsync(message, EditSinglePath).ConfigureAwait(false);

        public async Task<ActionResult> ViewUnsuspendList(UnsuspendDownlinesRequest message)
         => await ViewAsync(message, ViewUnsuspendListPath).ConfigureAwait(false);
    }
}