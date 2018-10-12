namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class DownlineListQuery : AccountInfoQueryBase, IRequest<DownlineListViewModel>
    {
        public DownlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}