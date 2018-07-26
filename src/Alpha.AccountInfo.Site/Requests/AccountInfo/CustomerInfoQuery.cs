namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        private readonly IUser viewer;

        public CustomerInfoQuery(IUser viewer)
            : base(viewer)
        {
            this.viewer = viewer;
        }

        public string UserName { get; set; }

        public int ActorId => viewer.Id;

        public byte ActorRoleId => viewer.LevelId;

        public UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(viewer.LevelId);

        public bool AllDayBalTransfer => viewer.AllowedAllDayBalTransfer;

        public bool AllowedTotalBalanceTransfer => viewer.AllowedTotalBalanceTransfer;

        public bool AllowTransfer => viewer.AllowTransfer;

        public bool ViewReports => viewer.CanViewReports;


        public override bool IsValid() => !string.IsNullOrEmpty(UserName) && ActorId > 0;
    }
}