namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
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
    }
}