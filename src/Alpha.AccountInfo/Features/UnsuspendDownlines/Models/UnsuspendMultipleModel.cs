namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System.Collections.Generic;

    public class UnsuspendMultipleByUserModel
    {
        public UnsuspendMultipleByUserModel(
            int custId,
            string username,
            bool suspended,
            IEnumerable<UnsuspendInfoModel> unsuspendByLevel)
        {
            CustId = custId;
            Username = username;
            Suspended = suspended;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int CustId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public IEnumerable<UnsuspendInfoModel> UnsuspendByLevel { get; private set; }
    }
}
