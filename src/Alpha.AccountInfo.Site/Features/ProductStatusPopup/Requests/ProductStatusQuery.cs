namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Products.Core.Shared;
    using MediatR;

    public class ProductStatusQuery : BaseRequest, IRequest<ProductStatusViewModel>
    {
        public ProductStatusQuery(IUser viewer)
            : base(viewer)
        {
            UplineId = viewer.Id;
            UplineLevel = Enumeration.FromValue<UserLevel>(viewer.LevelId);
            IsViewOnly = viewer.IsInternal || !viewer.CanUpdateAccount;
        }

        public int ProductId { get; set; }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustRoleId { get; set; }

        public UserLevel CustRole => Enumeration.FromValue<UserLevel>(CustRoleId);

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }

        public bool IsViewOnly { get; }

        public override bool IsValid()
            => ProductId > 0
            && CustId > 0
            && UserLevel.Member.Value <= CustRoleId
            && CustRoleId <= UserLevel.Master.Value;
    }
}