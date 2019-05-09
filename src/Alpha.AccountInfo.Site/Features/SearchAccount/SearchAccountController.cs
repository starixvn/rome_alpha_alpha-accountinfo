namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Web.Filters;
    using MediatR;
    using AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using System.Collections.Generic;
    using System.Linq;

    [SessionState(SessionStateBehavior.ReadOnly)]
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
        {
            var results = mediator.Send(message);
            var items = new List<UsernameSuggestion>();
            if (results.Result.Any())
            {
                foreach (var item in results.Result)
                {
                    if (!string.IsNullOrWhiteSpace(item.UserName2))
                    {
                        item.UserName = string.Format("{0} ({1})", item.UserName, item.UserName2);
                    }

                    items.Add(item);
                }
            }

            return Json(items, JsonRequestBehavior.AllowGet);
        }

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