namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProductStatusPopup.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using Alpha.Products.Core.Services;
    using Alpha.Products.Core.Shared;
    using MediatR;
    using System;

    public class ProductStatusQuery : BaseRequest, IRequest<ProductStatusViewModel>
    {
        private readonly Func<string, bool> hideProductStatusBySites;

        public ProductStatusQuery(IUser viewer) : this(viewer, null)
        {
        }

        public ProductStatusQuery(IUser viewer, Func<string, bool> hideProductStatusBySites)
            : base(viewer)
        {
            UplineId = viewer.Id;
            UplineLevel = Enumeration.FromValue<UserLevel>(viewer.LevelId);
            IsViewOnly = viewer.IsInternal || !viewer.CanUpdateAccount;
            Site = viewer.Site;
            this.hideProductStatusBySites = hideProductStatusBySites ?? SimpleServiceLocator.GetService<Func<string, bool>>();
        }

        public bool IsGroup { get; set; }

        public int ProductId { get; set; }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustRoleId { get; set; }

        public UserLevel CustRole => Enumeration.FromValue<UserLevel>(CustRoleId);

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }

        public bool IsViewOnly { get; }

        public string Site { get; }

        public override bool IsValid()
            => ProductId > 0
            && CustId > 0
            && UserLevel.Member.Value <= CustRoleId
            && CustRoleId <= UserLevel.Master.Value
            && !hideProductStatusBySites(Site);
    }
}