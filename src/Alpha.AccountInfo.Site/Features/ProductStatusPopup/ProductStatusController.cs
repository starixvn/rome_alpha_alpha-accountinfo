namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using Web.Filters;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ProductStatusController : AccountInfoBaseController
    {
        private const string ProductStatusPopupViewPath = "~/Features/ProductStatusPopup/Views/Index.cshtml";

        public ProductStatusController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProductStatusQuery message)
            => await ViewAsync(message, ProductStatusPopupViewPath);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Update(ProductStatusCommand message)
            => await JsonDataAsync<Result>(message, JsonRequestBehavior.DenyGet);
    }
}