namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
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

        public async Task<ActionResult> Index(CustomerInfoQuery message)
            => await View(message);

        //[HttpPost, AntiXsrfFilter]
        [HttpPost]
        public async Task<ActionResult> GetMasterBalanceInfoModelAsync(MasterBalanceInfoQuery message)
           => await JsonData(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public async Task<ActionResult> GetAgentBalanceInfoModelAsync(AgentBalanceInfoQuery message)
         => await JsonData<AgentBalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public async Task<ActionResult> GetMemberBalanceInfoModelAsync(MemberBalanceInfoQuery message)
         => await JsonData<MemberBalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);
    }
}