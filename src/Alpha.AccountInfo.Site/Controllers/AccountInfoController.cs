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

        public async Task<ActionResult> GetDownlineList(DownlineListQuery message)
          => await JsonData(message, JsonRequestBehavior.AllowGet);

        public async Task<ActionResult> GetCustomerStatus(ProductQuery message)
         => await JsonData(message, JsonRequestBehavior.AllowGet);
    }
}