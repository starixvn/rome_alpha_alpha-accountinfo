namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using AccountInfo.Models;
    using MediatR;
    using Requests.ProductStatus;

    [SessionState(SessionStateBehavior.Default)]
    public class ProductStatusController : AccountInfoBaseController
    {
        public ProductStatusController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProductStatusQuery message)
            => await View(message);

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductStatusCommand message)
            => await JsonData<Result>(message, JsonRequestBehavior.DenyGet);
    }
}