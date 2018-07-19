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
        public async Task<ActionResult> GetBalanceInfoModelAsync(BalanceInfoQuery message)
           => await JsonData<BalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);
    }
}