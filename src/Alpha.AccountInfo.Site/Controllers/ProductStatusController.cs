namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using AccountInfo.Models;
    using MediatR;
    using Requests.ProductStatus;
    using Web.Filters;

    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ProductStatusController : AccountInfoBaseController
    {
        public ProductStatusController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProductStatusQuery message)
            => await ViewAsync(message);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> Update(ProductStatusCommand message)
            => await JsonDataAsync<Result>(message, JsonRequestBehavior.DenyGet);
    }
}