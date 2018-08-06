namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using Alpha.Web;
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

        public ActionResult Index()
        {
            return View(new AccountInfoViewModel());
        }

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
    }
}