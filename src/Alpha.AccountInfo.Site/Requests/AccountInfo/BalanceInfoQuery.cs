namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class BalanceInfoQuery : BaseRequest, IRequest<BalanceInfoViewModel>
    {
        public BalanceInfoQuery(IUser viewer)
            : base(viewer)
        {
         
        }

        public int CustId { get; set; }

        public byte RoleId { get; set; }

        public override bool IsValid() => CustId > 0;
    }
}