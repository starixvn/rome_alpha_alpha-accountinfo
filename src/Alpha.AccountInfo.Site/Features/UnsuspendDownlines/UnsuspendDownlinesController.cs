namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class UnsuspendDownlinesController : AccountInfoBaseController
    {
        private const string EditSinglePath = "~/Features/UnsuspendDownlines/Views/EditSingle.cshtml";
        private const string EditMultiplePath = "~/Features/UnsuspendDownlines/Views/EditMultiple.cshtml";
        private const string ViewUnsuspendListPath = "~/Features/UnsuspendDownlines/Views/ViewList.cshtml";
        public UnsuspendDownlinesController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> EditSingle(UnsuspendSingleRequest message)
         => await ViewAsync(message, EditSinglePath).ConfigureAwait(false);

        public async Task<ActionResult> ViewUnsuspendList(UnsuspendViewListRequest message)
         => await ViewAsync(message, ViewUnsuspendListPath).ConfigureAwait(false);

        public async Task<ActionResult> EditMultiple(UnsuspendMultipleRequest message)
         => await ViewAsync(message, EditMultiplePath).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UnsuspendSingle(UnsuspendSingleCommand command)
          => JsonData(command, JsonRequestBehavior.DenyGet);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UnsuspendMultiple(UnsuspendMultipleCommand command)
          => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);
    }
}