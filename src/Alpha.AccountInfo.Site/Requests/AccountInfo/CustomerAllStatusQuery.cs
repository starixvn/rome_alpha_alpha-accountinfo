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
        }

        public int SearchAccountId { get; set; }

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

        public override bool IsValid() => SearchAccountId > 0;
    }
}