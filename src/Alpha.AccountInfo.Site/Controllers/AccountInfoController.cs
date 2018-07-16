namespace Alpha.AccountInfo.Site.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.SessionState;
    using MediatR;
    using ViewModels.AccountInfo;

    [SessionState(SessionStateBehavior.Default)]
    public class AccountInfoController : AccountInfoBaseController
    {
        public AccountInfoController(IMediator mediator)
            : base(mediator)
        {
        }

        public ActionResult Index()
        {
            var model = new AccountInfoViewModel(
                BuildCustomerInfoViewModel(),
                BuildBalanceInfoViewModel(),
                BuildDownlineListViewModel(),
                new StatusViewModel(),
                new SettingViewModel(),
                BuilLoginInfoViewModel(),
                new OnlineListViewModel()
                );

            return View(model);
        }


        private static CustomerInfoViewModel BuildCustomerInfoViewModel()
        {
            return new CustomerInfoViewModel(
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
        }

        private static BalanceInfoViewModel BuildBalanceInfoViewModel()
        {
            return new BalanceInfoViewModel(
                -4567487,
                -4566,
                -12856,
                -26563,
                15963,
                89632,
                1233,
                45678,
                89231,
                78971
                );
        }

        private static DownlineListViewModel BuildDownlineListViewModel()
        {
            var downlineList = new List<string>();

            for (int i = 1; i < 50; i++)
            {
                downlineList.Add($"user{i}");
            }


            return new DownlineListViewModel(
                downlineList,
                new TotalStatusDownline(8, 5, 6),
                new TotalStatusDownline(4, 7, 9)
                );
        }

        private static LoginInfoViewModel BuilLoginInfoViewModel()
        {
            return new LoginInfoViewModel(
                DateTime.Now,
                 DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                "192.168.1.1");
        }
    }
}