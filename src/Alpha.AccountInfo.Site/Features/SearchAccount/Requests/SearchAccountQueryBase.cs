namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;

    public class SearchAccountQueryBase : BaseRequest
    {
        private static readonly IEnumerable<byte> defaultLevelIdRanges = new List<byte>
                {
                    UserLevel.Master.Value,
                    UserLevel.Agent.Value,
                    UserLevel.Member.Value
                };

        protected readonly IEnumerable<byte> SearchAccountRoleIdRange;

        public SearchAccountQueryBase(IUser viewer) : base(viewer)
        {
            SearchAccountRoleIdRange = defaultLevelIdRanges;
        }

        public int SearchAccountId { get; set; }

        public override bool IsValid() => SearchAccountId > 0;

        internal bool IsValidSearchAccountRoleId(byte searchAccountRoleId)
            => SearchAccountRoleIdRange.Contains(searchAccountRoleId);
    }
}