namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Fanex.Common;
    using MediatR;

    public class MultipleCustomerStatusQuery : CustomerStatusQuery, IRequest<CustomerStatusViewModel>
    {
        public MultipleCustomerStatusQuery(IUser viewer) : base(viewer)
        {
        }

        public string CustomerIds { get; set; } = "1176083, 33278659";

        public string Usernames { get; set; } = "master1, master1p1";

        public override string SearchAccountName
        {
            get
            {
                var usernames = Usernames.Split(',');

                return usernames[0];
            }
        }

        public override int SearchAccountId
        {
            get
            {
                var customerIds = CustomerIds.Split(',');

                return Converter.ToInt(customerIds[0]);
            }
        }

        public override bool IsValid() => 
            base.IsValid() &&
            !string.IsNullOrEmpty(CustomerIds) && 
            !string.IsNullOrEmpty(Usernames);
    }
}