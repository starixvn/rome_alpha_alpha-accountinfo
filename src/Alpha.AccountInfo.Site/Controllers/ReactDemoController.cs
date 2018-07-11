namespace Alpha.AccountInfo.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using ViewModels.AccountInfo;

    public class ReactDemoController : Controller
    {
        public ReactDemoController(IMediator mediator)
        {
        }

        public ActionResult Index()
        {
            var model = new CustomerInfoViewModel(
                    12,
                    1,
                    4,
                    "Member01",
                    "Member01",
                    "08 22233",
                    "08 444433",
                    "Member01",
                    "Agent01",
                    "Master1",
                    "Supertest1"
                );

            return View(model);
        }
    }
}