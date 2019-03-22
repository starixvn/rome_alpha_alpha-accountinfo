namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;

    public class SearchAccountQueryBase : BaseRequest
    {
        private readonly IUser viewer;

        public SearchAccountQueryBase(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public int SearchAccountId { get; set; }

        public override bool IsValid() => SearchAccountId > 0 && !SearchAccountId.Equals(viewer.Id);

        internal bool IsValidSearchAccountRoleId(byte searchAccountRoleId)
            => searchAccountRoleId < viewer.LevelId;

        internal bool CanUpdateAccount => viewer.CanUpdateAccount;

        internal bool IsInternal => viewer.IsInternal;
    }
}