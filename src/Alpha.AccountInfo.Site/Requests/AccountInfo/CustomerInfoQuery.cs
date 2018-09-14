namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        private readonly IUser viewer;

        public CustomerInfoQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public int ActorId => viewer.Id;

        public byte ActorRoleId => viewer.LevelId;

        public bool AllDayBalTransfer => viewer.AllowedAllDayBalTransfer;

        public bool AllowedTotalBalanceTransfer => viewer.AllowedTotalBalanceTransfer;

        public bool AllowTransfer => viewer.AllowTransfer;

        public bool ViewReports => viewer.CanViewReports;

        public override bool IsValid() =>
            IsValidName(SearchAccountName) && 
            SearchAccountName != viewer.Name;
    }
}