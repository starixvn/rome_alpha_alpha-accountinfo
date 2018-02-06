namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Enumerations;
    using MediatR;
    using ViewModels.ProductStatus;

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
        {
            return ProductId > 0 && CustId > 0 && UserLevel.Member <= CustRole && CustRole <= UserLevel.Master;
        }
    }
}