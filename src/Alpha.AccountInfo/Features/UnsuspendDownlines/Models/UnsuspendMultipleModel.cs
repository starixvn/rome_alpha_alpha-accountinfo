namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System.Collections.Generic;

    public class UnsuspendMultipleModel
    {
        public UnsuspendMultipleModel(
            int custId,
            byte searchAccountRoleId,
            string username,
            bool suspended,
            IEnumerable<UnsuspendMultipleDownline> unsuspendByLevel)
        {
            CustId = custId;
            SearchAccountRoleId = searchAccountRoleId;
            Username = username;
            Suspended = suspended;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int CustId { get; private set; }

        public byte SearchAccountRoleId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public IEnumerable<UnsuspendMultipleDownline> UnsuspendByLevel { get; private set; }
    }

    public class UnsuspendMultipleSearchAccount
    { 
        public UnsuspendMultipleSearchAccount() { }

        public UnsuspendMultipleSearchAccount(
            int custId,
            string username,
            bool suspended)
        {
            CustId = custId;
            Username = username;
            Suspended = suspended;
        }

        public int CustId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }
    }

    public class UnsuspendMultipleDownline
    {
        public UnsuspendMultipleDownline() { }

        public UnsuspendMultipleDownline(
            int custId,
            byte downlineRoleId,
            int affectedDownlines, 
            int unchangedDownlines)
        {
            CustId = custId;
            DownlineRoleId = downlineRoleId;
            AffectedDownlines = affectedDownlines;
            UnchangedDownlines = unchangedDownlines;
        }

        public int CustId { get; private set; }

        public byte DownlineRoleId { get; private set; }

        public int AffectedDownlines { get; private set; }

        public int UnchangedDownlines { get; private set; }
    }
}
