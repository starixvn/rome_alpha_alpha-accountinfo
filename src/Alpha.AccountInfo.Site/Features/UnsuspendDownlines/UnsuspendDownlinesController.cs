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
        public UnsuspendDownlinesController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> EditSingle(UnsuspendSingleRequest message)
         => await ViewAsync(message, EditSinglePath).ConfigureAwait(false);

        public async Task<ActionResult> EditMultiple(UnsuspendMultipleRequest message)
         => await ViewAsync(message, EditMultiplePath).ConfigureAwait(false);

        public async Task<ActionResult> FetchCustomerList(UnsuspendViewListRequest message)
           => await JsonDataAsync(message).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UnsuspendSingle(UnsuspendSingleCommand command)
          => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UnsuspendMultiple(UnsuspendMultipleCommand command)
          => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);
    }
}