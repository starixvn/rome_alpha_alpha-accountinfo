namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    public class UnsuspendMultipleByUserModel
    {
        public UnsuspendMultipleByUserModel() { }

        public UnsuspendMultipleByUserModel(
            int custId,
            string username,
            bool suspended,
            byte roleId,
            int affectedDownlines, 
            int unchangedDownlines)
        {
            CustId = custId;
            Username = username;
            Suspended = suspended;
            RoleId = roleId;
            AffectedDownlines = affectedDownlines;
            UnchangedDownlines = unchangedDownlines;
        }

        public int CustId { get; private set; }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public byte RoleId { get; private set; }

        public int AffectedDownlines { get; private set; }

        public int UnchangedDownlines { get; private set; }
    }
}
