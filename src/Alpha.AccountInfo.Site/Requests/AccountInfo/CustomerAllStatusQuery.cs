namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class CustomerAllStatusQuery : BaseRequest, IRequest<CustomererAllStatusViewModel>
    {
        private readonly IUser viewer;

        public CustomerAllStatusQuery(IUser viewer)
            : base(viewer)
        {
            this.viewer = viewer;
            ActorId = viewer.Id;
            ActorRoleId = viewer.LevelId;
            ActorName = viewer.Name;

            SubAccountId = viewer.SubAccId;
            CanUpdateAccount = !viewer.IsInternal && viewer.CanUpdateAccount;

            IsHideKeno = viewer.IsHideKeno;
            IsCasinoSync = viewer.IsCasinoSync;
            IsCasinoDisabled = viewer.IsCasinoDisabled;

            IsHideColossusBets = viewer.IsHideColossusBets;
            DisableColossusBets = viewer.DisableColossusBets;

            IsDisabledESports = viewer.IsDisabledESports;
            IsNumberGameDisabled = viewer.IsNumberGameDisabled;
        }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public int UplientId { get; set; }

        public string UplineName { get; set; }

        public byte UplineRoleId { get; set; }

        public int ActorId { get; }

        public byte ActorRoleId { get; }

        public string ActorName { get; }

        public int SubAccountId { get; }

        public bool CanUpdateAccount { get; }

        public bool GetDisabledProduct(int productId) => viewer.GetDisabledProduct(productId);

        public bool GetHideProduct(int productId) => viewer.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => viewer.GetSyncedProduct(productId);

        public bool IsHideKeno { get; }

        public bool IsCasinoSync { get; }

        public bool IsCasinoDisabled { get; }

        public bool IsHideColossusBets { get; }

        public bool DisableColossusBets { get; }

        public bool IsDisabledESports { get; }

        public bool IsNumberGameDisabled { get; }

        public override bool IsValid() => SearchAccountId > 0;
    }
}