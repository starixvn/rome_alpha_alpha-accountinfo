namespace Alpha.AccountInfo.Site.Features.Shared
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using Web;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods

    public abstract class AccountInfoBaseController : BaseAsyncController
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
    {
        private const string NoInformationPageViewPath = "~/Features/Shared/Views/NoInformation.cshtml";

        protected readonly IMediator mediator;

        protected AccountInfoBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message, string viewPath)
            => (message as BaseRequest).IsValid()
               ? View(viewPath, await mediator.Send(message))
               : View(NoInformationPageViewPath);

        protected ActionResult JsonData<TResponse>(
           IRequest<TResponse> message,
           JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            if (!(message as BaseRequest).IsValid())
            {
                return Json(Result.CreateForbiddenResult(), jsonRequestBehavior);
            }

            return Json(mediator.Send(message), jsonRequestBehavior);
        }

        protected async Task<ActionResult> JsonDataAsync<TResponse>(
            IRequest<TResponse> message,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
        {
            if (!(message as BaseRequest).IsValid())
            {
                return Json(Result.CreateForbiddenResult(), jsonRequestBehavior);
            }

            return Json(await mediator.Send(message), jsonRequestBehavior);
        }
    }
}