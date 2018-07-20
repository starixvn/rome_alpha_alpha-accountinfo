namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class MasterBalanceInfoQuery : BalanceInfoQuery, IRequest<MasterBalanceInfoViewModel>
    {
        public MasterBalanceInfoQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}