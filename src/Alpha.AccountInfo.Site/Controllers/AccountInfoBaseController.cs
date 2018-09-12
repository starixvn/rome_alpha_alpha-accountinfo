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
            => (message as BaseRequest).IsValid() ? 
                View(mediator.Send(message)) : View(NoInformationPageViewPath);

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
             => (message as BaseRequest).IsValid() ?
                View(await mediator.Send(message)) : View(NoInformationPageViewPath);

        protected ActionResult JsonData<TResponse>(
           IRequest<TResponse> message,
           JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            if (!(message as BaseRequest).IsValid())
            {
                return Json(Result.CreateForbiddenResult());
            }

            return Json(mediator.Send(message), jsonRequestBehavior);
        }

        protected async Task<ActionResult> JsonDataAsync<TResponse>(
            IRequest<TResponse> message,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            if (!(message as BaseRequest).IsValid())
            {
                return Json(Result.CreateForbiddenResult());
            }

            return Json(await mediator.Send(message), jsonRequestBehavior);
        }
    }
}