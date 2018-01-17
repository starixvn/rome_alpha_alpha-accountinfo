namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using MediatR;
    using Web;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed")]
    public abstract class AccountInfoBaseController : BaseAsyncController
    {
        protected readonly IMediator mediator;

        protected AccountInfoBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
        {
            return View(mediator.Send(message));
        }
    }
}