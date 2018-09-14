namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AccountInfoQueryBase : BaseRequest
    {
        protected readonly IEnumerable<byte> SearchAccountRoleIdRange;
            
        public AccountInfoQueryBase(IUser viewer) : base(viewer)
        {
            SearchAccountRoleIdRange = new List<byte>
                {
                    UserLevel.Master.Value,
                    UserLevel.Agent.Value,
                    UserLevel.Member.Value
                };
        }

        public int SearchAccountId { get; set; }

        public override bool IsValid() => SearchAccountId > 0;

        internal bool IsValidSearchAccountRoleId(byte searchAccountRoleId)
            => SearchAccountRoleIdRange.Contains(searchAccountRoleId);
    }
}