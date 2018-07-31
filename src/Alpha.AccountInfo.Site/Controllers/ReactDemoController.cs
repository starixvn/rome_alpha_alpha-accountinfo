//namespace Alpha.AccountInfo.Site.Controllers
//{
//    using Alpha.AccountInfo.Site.Requests.ProductStatus;
//    using MediatR;
//    using System.Threading.Tasks;
//    using System.Web.Mvc;
//    using ViewModels.AccountInfo;

//    public class ReactDemoController : AccountInfoBaseController
//    {
//        public ReactDemoController(IMediator mediator)
//            : base(mediator)
//        {
//        }

//        public ActionResult Index() => View(new AccountInfoViewModel());

//        public async Task<ActionResult> GetCustomerInfoAsync(CustomerInfoQuery message)
//           => await JsonData(message, JsonRequestBehavior.DenyGet);

//        public async Task<ActionResult> GetCustomerInfoAsync00(CustomerInfoQuery message)
//            => await JsonData(message, JsonRequestBehavior.DenyGet);

//        //[HttpPost, AntiXsrfFilter]
//        [HttpPost]
//        public async Task<ActionResult> GetMasterBalanceInfoModelAsync(MasterBalanceInfoQuery message)
//           => await JsonData(message, JsonRequestBehavior.DenyGet);

//        [HttpPost]
//        public async Task<ActionResult> GetAgentBalanceInfoModelAsync(AgentBalanceInfoQuery message)
//         => await JsonData<AgentBalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);

//        [HttpPost]
//        public async Task<ActionResult> GetMemberBalanceInfoModelAsync(MemberBalanceInfoQuery message)
//         => await JsonData<MemberBalanceInfoViewModel>(message, JsonRequestBehavior.DenyGet);
//    }
//}