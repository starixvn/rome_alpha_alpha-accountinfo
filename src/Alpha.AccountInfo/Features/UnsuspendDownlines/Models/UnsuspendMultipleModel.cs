namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System;
    using System.Collections.Generic;

    public class UnsuspendMultipleModel
    {
        public UnsuspendMultipleModel(
            int custId,
            byte searchAccountRoleId,
            string username,
            bool suspended,
            IEnumerable<UnsuspendDownlineModel> downlines)
        {
            CustId = custId;
            SearchAccountRoleId = searchAccountRoleId;
            Username = username;
            Suspended = suspended;
            Downlines = downlines;
        }

        public int CustId { get; private set; }

        public byte SearchAccountRoleId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public IEnumerable<UnsuspendDownlineModel> Downlines { get; private set; }

        internal UnsuspendMultipleModel ToList()
        {
            throw new NotImplementedException();
        }
    }

    public class UnsuspendSearchAccountModel
    { 
        public UnsuspendSearchAccountModel() { }

        public UnsuspendSearchAccountModel(
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

    public class UnsuspendDownlineModel
    {
        public UnsuspendDownlineModel() { }

        public UnsuspendDownlineModel(
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
