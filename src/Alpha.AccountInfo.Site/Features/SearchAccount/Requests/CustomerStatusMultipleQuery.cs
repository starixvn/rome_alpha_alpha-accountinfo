namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.Products.AccountInfo.Setting.ViewModels.Base;
    using Fanex.Common;
    using MediatR;
    using System.Collections.Generic;

    public class CustomerStatusMultipleQuery : CustomerStatusQueryBase, IRequest<IEnumerable<ISettingItemMultipleViewModel>>
    {
        public CustomerStatusMultipleQuery(IUser viewer) : base(viewer)
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