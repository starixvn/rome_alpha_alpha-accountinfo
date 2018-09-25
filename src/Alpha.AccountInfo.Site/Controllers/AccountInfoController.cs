namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Web.Filters;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class AccountInfoController : AccountInfoBaseController
    {
        public AccountInfoController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(AccountInfoQuery message)
          => await ViewAsync(message);

        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
           => JsonData(message);

        public ActionResult GetCustomerInfo(CustomerInfoQuery message)
            => JsonData(message);

        public ActionResult GetDownlineList(DownlineListQuery message)
            =>  JsonData(message);

        public ActionResult GetCustomerAllStatus(CustomerAllStatusQuery message)
            => JsonData(message);

        public ActionResult GetOnlineList(OnlineListQuery message)
           => JsonData(message);

        public async Task<ActionResult> GivenCredit(GivenCreditQuery message)
           => await ViewAsync(message);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateGivenCredit(GivenCreditCommand command)
          => JsonData(command, JsonRequestBehavior.DenyGet);

        [HttpPost, AntiXsrfFilter]
        public ActionResult UpdateCustomerStatus(CustomerStatusCommand command)
           => JsonData(command, JsonRequestBehavior.DenyGet);
    }
}