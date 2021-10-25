namespace Alpha.AccountInfo.Site.Features.ProtectedStatus
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Site.Features.ProtectedStatus.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;

    public class ProtectedStatusController : AccountInfoBaseController
    {
        private const string EditMultiplePath = "~/Features/ProtectedStatus/Views/EditMultiple.cshtml";

        public ProtectedStatusController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<ActionResult> EditMultiple(EditMultipleRequest message)
            => await ViewAsync(message, EditMultiplePath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> EditMultiple(EditMultipleCommand command)
            => await JsonDataAsync(command);
    }
}