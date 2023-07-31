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
        private const string MemberBalanceInfoViewPath = "~/Features/SearchAccount/Views/GetMemberBalanceInfo.cshtml";

        public SearchAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(SearchAccountQuery message)
          => await ViewAsync(message, SearchAccountViewPath);

        public async Task<ActionResult> GetCustomerInfo(CustomerInfoQuery message)
            => await JsonDataAsync(message);

        public async Task<ActionResult> GetDownlineList(DownlineListQuery message)
            => await JsonDataAsync(message);

        public async Task<ActionResult> GetOnlineList(OnlineListQuery message)
           => await JsonDataAsync(message);

        public async Task<ActionResult> GivenCredit(GivenCreditQuery message)
           => await ViewAsync(message, GiventCreditViewPath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateGivenCredit(GivenCreditCommand command)
          => await JsonDataAsync(command, JsonRequestBehavior.DenyGet);

        [HttpPost]
        public async Task<ActionResult> UpdateCustomerStatus(CustomerNonProductStatusCommand command)
           => await JsonDataAsync(command, JsonRequestBehavior.DenyGet);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateAllDayBalanceTransfer(UpdateAllBalanceTransferCommand command)
           => await JsonDataAsync(command, JsonRequestBehavior.DenyGet);

        public async Task<ActionResult> GetMemberBalanceInfo(MemberBalanceInfoQuery message)
            => await ViewAsync(message, MemberBalanceInfoViewPath);
    }
}