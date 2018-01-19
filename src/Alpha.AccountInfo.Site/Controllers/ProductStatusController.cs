namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.SessionState;
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

            var viewModel = await mediator.SendAsync(message);

            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(ProductStatusCommand command)
        {
            if (command == null || !command.IsValid())
            {
                return View(InvalidPageViewPath);
            }

            var result = await mediator.SendAsync(command);

            return Json(result, JsonRequestBehavior.DenyGet);
        }
    }
}