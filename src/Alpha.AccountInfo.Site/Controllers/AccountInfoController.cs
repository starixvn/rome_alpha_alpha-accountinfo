namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;

    [SessionState(SessionStateBehavior.Default)]
    public class AccountInfoController : AccountInfoBaseController
    {
        public AccountInfoController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index() => View();

        public ActionResult React() => View();
    }
}