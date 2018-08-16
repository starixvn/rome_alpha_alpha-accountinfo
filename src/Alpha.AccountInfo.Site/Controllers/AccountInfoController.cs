namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using MediatR;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;

    [SessionState(SessionStateBehavior.Default)]
    public class AccountInfoController : AccountInfoBaseController
    {
        protected readonly IMediator mediator;

        public AccountInfoController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(AccountInfoQuery message)
          => await ViewAsync(message);

        [HttpPost]
        public ActionResult GetUsernamesSuggestion(UserNameSuggestionQuery message)
           => JsonData(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult GetCustomerInfo(CustomerInfoQuery message)
            => JsonData(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult GetDownlineList(DownlineListQuery message)
            =>  JsonData(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult GetCustomerAllStatus(CustomerAllStatusQuery message)
            => JsonData(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult GetOnlineList(OnlineListQuery message)
           => JsonData(message, JsonRequestBehavior.DenyGet);

        public async Task<ActionResult> GivenCredit(GivenCreditQuery message)
           => await ViewAsync(message);

        [HttpPost]
        public ActionResult UpdateGivenCredit(GivenCreditCommand command)
          => JsonData(command, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult UpdateOutright(OutrightCommand command)
           => JsonData(command, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public ActionResult UpdateClosed(ClosedCommand command)
          => JsonData(command, JsonRequestBehavior.DenyGet);
    }
}