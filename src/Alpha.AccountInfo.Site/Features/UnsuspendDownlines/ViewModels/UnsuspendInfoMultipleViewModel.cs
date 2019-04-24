namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using System.Collections.Generic;

    public class UnsuspendInfoMultipleViewModel
    {
        public UnsuspendInfoMultipleViewModel(
            int custId,
            string username,
            bool suspended,
            byte roleId,
            IEnumerable<UnsuspendMultipleByUserModel> unsuspendByLevel)
        {
            CustId = custId;
            Username = username;
            Suspended = suspended;
            RoleId = roleId;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int CustId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public byte RoleId { get; private set; }

        public IEnumerable<UnsuspendMultipleByUserModel> UnsuspendByLevel { get; private set; }
    }
}
