namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class MemberBalanceInfoQuery : BalanceInfoQuery, IRequest<MemberBalanceInfoViewModel>
    {
        public MemberBalanceInfoQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}