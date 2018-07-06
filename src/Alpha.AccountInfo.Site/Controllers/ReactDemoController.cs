namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;

    public class ReactDemoController : Controller
    {
        public ReactDemoController(IMediator mediator)
        {
        }

        public ActionResult Index() => View();
    }
}