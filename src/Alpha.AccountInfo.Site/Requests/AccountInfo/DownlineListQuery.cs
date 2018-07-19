namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class DownlineListQuery : BaseRequest, IRequest<DownlineListViewModel>
    {
        public DownlineListQuery(IUser viewer)
            : base(viewer)
        {
         
        }
        public int CustId { get; set; }

        public override bool IsValid() => CustId > 0;
    }
}