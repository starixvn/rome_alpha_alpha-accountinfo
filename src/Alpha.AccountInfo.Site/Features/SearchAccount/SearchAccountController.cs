namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class SearchAccountController : AccountInfoBaseController
    {
        private const string SearchAccountViewPath = "~/Features/SearchAccount/Views/Index.cshtml";
        private const string GiventCreditViewPath = "~/Features/SearchAccount/Views/GivenCredit.cshtml";
        private const string EditPopupViewPath = "~/Features/SearchAccount/Views/EditPopup.cshtml";

        public SearchAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(SearchAccountQuery message)
          => await ViewAsync(message, SearchAccountViewPath).ConfigureAwait(false);

        public async Task<ActionResult> GetUsernamesSuggestion(UserNameSuggestionQuery message)
           => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> GetCustomerInfo(CustomerInfoQuery message)
            => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> GetDownlineList(DownlineListQuery message)
            => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> GetCustomerAllStatus(CustomerStatusQuery message)
            => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> GetOnlineList(OnlineListQuery message)
           => await JsonDataAsync(message).ConfigureAwait(false);

        public async Task<ActionResult> GivenCredit(GivenCreditQuery message)
           => await ViewAsync(message, GiventCreditViewPath).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateGivenCredit(GivenCreditCommand command)
          => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateCustomerStatus(CustomerNonProductStatusCommand command)
           => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateAllDayBalanceTransfer(UpdateAllBalanceTransferCommand command)
           => await JsonDataAsync(command, JsonRequestBehavior.DenyGet).ConfigureAwait(false);

        public async Task<ActionResult> EditPopup(CustomerStatusQuery message)
          => await ViewAsync(message, EditPopupViewPath).ConfigureAwait(false);
    }
}