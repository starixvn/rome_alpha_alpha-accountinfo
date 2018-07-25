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

        public async Task<ActionResult> GetCustomerInfo(CustomerInfoQuery message)
            => await JsonData(message, JsonRequestBehavior.AllowGet);

        //[HttpPost, AntiXsrfFilter]
        [HttpPost]
        public async Task<ActionResult> GetMasterBalanceInfo(MasterBalanceInfoQuery message)
        {
            return Json(await mediator.Send(message), JsonRequestBehavior.AllowGet);
        }
        // => await JsonData(message, JsonRequestBehavior.DenyGet);

        //[HttpPost]
        //public async Task<ActionResult> GetAgentBalanceInfoModelAsync(AgentBalanceInfoQuery message)
        // => await JsonData<AgentBalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public async Task<ActionResult> GetMemberBalanceInfo(MemberBalanceInfoQuery message)
         => await JsonData<MemberBalanceInfoViewModel>(message, JsonRequestBehavior.AllowGet);

        public async Task<ActionResult> GetDownlineList(DownlineListQuery message)
          => await JsonData(message, JsonRequestBehavior.AllowGet);
    }
}