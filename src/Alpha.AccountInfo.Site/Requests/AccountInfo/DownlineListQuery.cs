namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class DownlineListQuery : AccountInfoQueryBase, IRequest<DownlineListViewModel>
    {
        public DownlineListQuery(IUser viewer) : base(viewer)
        {
        }
    }
}