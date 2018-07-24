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

        public int UplineId => viewer.Id;

        public UserLevel UplineLevel => Enumeration.FromValue<UserLevel>(viewer.LevelId);

        public override bool IsValid() => !string.IsNullOrEmpty(UserName) && UplineId > 0;
    }
}