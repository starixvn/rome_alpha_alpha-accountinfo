namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class CustomerStatusQuery : CustomerStatusQueryBase, IRequest<CustomerStatusViewModel>
    {
        public CustomerStatusQuery(IUser viewer) : base(viewer)
        {
        }
    }
}