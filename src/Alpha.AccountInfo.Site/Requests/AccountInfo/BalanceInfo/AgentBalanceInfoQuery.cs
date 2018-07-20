namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class AgentBalanceInfoQuery : BalanceInfoQuery, IRequest<AgentBalanceInfoViewModel>
    {
        public AgentBalanceInfoQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}