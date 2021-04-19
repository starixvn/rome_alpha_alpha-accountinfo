namespace Alpha.AccountInfo.Site.Features.OpenDownlines
{
    using Alpha.Web.Filters;
    using MediatR;
    using Requests;
    using Shared;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class OpenDownlinesController : AccountInfoBaseController
    {
        private const string EditSinglePath = "~/Features/OpenDownlines/Views/EditSingle.cshtml";
        private const string EditMultiplePath = "~/Features/OpenDownlines/Views/EditMultiple.cshtml";

        public OpenDownlinesController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<ActionResult> EditSingle(OpenSingleRequest message)
            => await ViewAsync(message, EditSinglePath).ConfigureAwait(false);

        public async Task<ActionResult> EditMultiple(OpenMultipleRequest message)
         => await ViewAsync(message, EditMultiplePath).ConfigureAwait(false);

        public async Task<ActionResult> FetchCustomerList(OpenViewListRequest message)
           => await JsonDataAsync(message).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> OpenSingle(OpenSingleCommand command)
          => await JsonDataAsync(command).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> OpenMultiple(OpenMultipleCommand command)
          => await JsonDataAsync(command).ConfigureAwait(false);
    }
}