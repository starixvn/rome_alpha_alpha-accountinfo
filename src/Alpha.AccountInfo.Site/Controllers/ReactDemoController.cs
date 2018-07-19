namespace Alpha.AccountInfo.Site.Controllers
{
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using ViewModels.AccountInfo;

    public class ReactDemoController : AccountInfoBaseController
    {
        public ReactDemoController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(CustomerInfoQuery message)
            => await View(message);
    }
}