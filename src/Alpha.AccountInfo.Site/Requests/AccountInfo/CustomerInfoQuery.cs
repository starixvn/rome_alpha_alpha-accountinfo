namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Enumerations;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        public CustomerInfoQuery(IUser viewer)
            : base(viewer)
        {
            UplineLevel = Enumeration.FromValue<UserLevel>(viewer.LevelId);
        }

        public string UserName { get; set; }

        public int UplineId { get; set; }

        public UserLevel UplineLevel { get; }

        public override bool IsValid() => !string.IsNullOrEmpty(UserName) && UplineId > 0;
    }
}