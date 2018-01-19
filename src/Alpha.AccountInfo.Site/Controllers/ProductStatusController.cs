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
        private const string InvalidPageViewPath = "~/Views/Shared/NoInformation.cshtml";

        public ProductStatusController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(ProductStatusQuery message)
        {
            if (message == null || !message.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            var viewModel = await mediator.Send(message);

            if (viewModel == null)
            {
                return View(InvalidPageViewPath);
            }

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductStatusCommand message)
        {
            if (message == null || !message.IsValid() || message.IsViewOnly)
            {
                return Json(Result.CreateForbiddenResult());
            }

            var result = await mediator.Send<Result>(message);

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}