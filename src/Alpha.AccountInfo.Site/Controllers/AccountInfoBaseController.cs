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

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
        {
            var baseRequest = message as BaseRequest;
            var isValid = baseRequest?.IsValid();

            if (isValid != true)
            {
                return View(NoInformationPageViewPath);
            }

            return View(mediator.Send(message));
        }


        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
        {
            var baseRequest = message as BaseRequest;
            var isValid = baseRequest?.IsValid();

            if (isValid != true)
            {
                return View(NoInformationPageViewPath);
            }

            return View(await mediator.Send(message));
        }

        protected ActionResult JsonData<TResponse>(
           IRequest<TResponse> message,
           JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            var baseRequest = message as BaseRequest;
            var isValid = baseRequest?.IsValid();

            if (isValid != true)
            {
                return Json(Result.CreateForbiddenResult());
            }

            var model = mediator.Send(message);
            return Json(model, jsonRequestBehavior);
        }

        protected async Task<ActionResult> JsonDataAsync<TResponse>(
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