namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;

    [SessionState(SessionStateBehavior.Default)]
    public class SearchAccountController : AccountInfoBaseController
    {
        private const string SearchAccountViewPath = "~/Features/SearchAccount/Views/Index.cshtml";
        private const string GiventCreditViewPath = "~/Features/SearchAccount/Views/GivenCredit.cshtml";

        public SearchAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(SearchAccountQuery message)
          => await ViewAsync(message, SearchAccountViewPath).ConfigureAwait(false);

        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
           => JsonData(message);

        public ActionResult GetCustomerInfo(CustomerInfoQuery message)
            => JsonData(message);

        public ActionResult GetDownlineList(DownlineListQuery message)
            => JsonData(message);

        public ActionResult GetCustomerAllStatus(CustomerStatusQuery message)
            => JsonData(message);

        public ActionResult GetOnlineList(OnlineListQuery message)
           => JsonData(message);

        public async Task<ActionResult> GivenCredit(GivenCreditQuery message)
           => await ViewAsync(message, GiventCreditViewPath).ConfigureAwait(false);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateGivenCredit(GivenCreditCommand command)
          => JsonData(command, JsonRequestBehavior.DenyGet);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCustomerStatus(CustomerNonProductStatusCommand command)
           => JsonData(command, JsonRequestBehavior.DenyGet);
    }
}