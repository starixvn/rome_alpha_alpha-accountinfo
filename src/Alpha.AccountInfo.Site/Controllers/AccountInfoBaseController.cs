namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AccountInfo.Models;
    using MediatR;
    using Requests;
    using Web;

    public abstract class AccountInfoBaseController : BaseAsyncController
    {
        private const string NoInformationPageViewPath = "~/Views/Shared/NoInformation.cshtml";

        protected readonly IMediator mediator;

        protected AccountInfoBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> View<TResponse>(IRequest<TResponse> message)
        {
            var baseRequest = message as BaseRequest;
            var isValid = baseRequest?.IsValid();

            if (isValid != true)
            {
                return View(NoInformationPageViewPath);
            }

            return View(await mediator.Send(message));
        }

        protected async Task<ActionResult> JsonData<TResponse>(
            IRequest<TResponse> message,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            var baseRequest = message as BaseRequest;
            var isValid = baseRequest?.IsValid();

            if (isValid != true)
            {
                return Json(Result.CreateForbiddenResult());
            }

            return Json(await mediator.Send(message), jsonRequestBehavior);
        }
    }
}