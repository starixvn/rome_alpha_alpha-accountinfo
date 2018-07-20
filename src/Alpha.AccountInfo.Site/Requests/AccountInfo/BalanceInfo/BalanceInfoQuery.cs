namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class BalanceInfoQuery : BaseRequest, IRequest<MasterBalanceInfoViewModel>
    {
        private readonly IUser viewer;

        public BalanceInfoQuery(IUser viewer)
            : base(viewer)
        {
            this.viewer = viewer;
        }

        public string UserName { get; set; }

        public int CustId { get; set; }

        public byte RoleId { get; set; }

        public byte ActorId => viewer.LevelId;

        public bool AllDayBalTransfer => viewer.AllowedAllDayBalTransfer;

        public bool AllowedTotalBalanceTransfer => viewer.AllowedTotalBalanceTransfer;

        public bool AllowTransfer => viewer.AllowTransfer;

        public bool ViewReports => viewer.CanViewReports;

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(UserName);
    }
}