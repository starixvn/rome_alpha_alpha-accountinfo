namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class EditProductController : AccountInfoBaseController
    {
        private const string EditPopupViewPath = "~/Features/SearchAccount/Views/EditPopup.cshtml";
        private const string EditPopupMultiplePath = "~/Features/SearchAccount/Views/EditPopupMultiple.cshtml";

        public EditProductController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> GetCustomerAllStatus(CustomerStatusQuery message)
            => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> EditPopup(CustomerStatusQuery message)
          => await ViewAsync(message, EditPopupViewPath).ConfigureAwait(false);

        public async Task<ActionResult> MultipleEditPopup(CustomerStatusMultipleQuery message)
          => await ViewAsync(message, EditPopupMultiplePath).ConfigureAwait(false);
    }
}