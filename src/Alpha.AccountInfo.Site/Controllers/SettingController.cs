namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Runtime.Remoting.Contexts;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;

    [SessionState(SessionStateBehavior.Default)]
    public class SettingController : AccountInfoBaseController
    {
        public SettingController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            //HttpContext.Response.AddHeader("Content-type", "text/plain");
            //HttpContext.Response.AddHeader("Content-type", "text/plain");

            HttpContext.Response.ContentType = "text/html; application/json; charset=utf-8";

            return PartialView();
        }

        //  public ActionResult Index() => View();
    }
}