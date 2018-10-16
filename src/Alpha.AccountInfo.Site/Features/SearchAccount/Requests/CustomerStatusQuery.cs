namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class CustomerStatusQuery : SearchAccountQueryBase, IRequest<CustomererStatusViewModel>
    {
        private readonly IUser viewer;

        public CustomerStatusQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public int UplineId { get; set; }

        public string UplineName { get; set; }

        public byte UplineRoleId { get; set; }

        public int ActorId => viewer.Id;

        public byte ActorRoleId => viewer.LevelId;

        public string ActorName => viewer.Name;

        public int SubAccountId => viewer.SubAccId;

        public bool CanUpdateAccount => !viewer.IsInternal && viewer.CanUpdateAccount;

        public bool GetDisabledProduct(int productId) => viewer.GetDisabledProduct(productId);

        public bool GetHideProduct(int productId) => viewer.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => viewer.GetSyncedProduct(productId);

        public bool IsHideKeno => viewer.IsHideKeno;

        public bool IsCasinoSync => viewer.IsCasinoSync;

        public bool IsCasinoDisabled => viewer.IsCasinoDisabled;

        public bool IsHideColossusBets => viewer.IsHideColossusBets;

        public bool DisableColossusBets => viewer.DisableColossusBets;

        public bool IsDisabledESports => viewer.IsDisabledESports;

        public bool IsNumberGameDisabled => viewer.IsNumberGameDisabled;

        public override bool IsValid() => IsValidSearchAccount && IsValidUpline;

        internal bool IsValidSearchAccount =>
            base.IsValid() &&
            IsValidName(SearchAccountName) &&
            IsValidSearchAccountRoleId(SearchAccountRoleId);

        internal bool IsValidUpline =>
            IsValidName(UplineName) &&
            UplineRoleId > SearchAccountRoleId;
    }
}