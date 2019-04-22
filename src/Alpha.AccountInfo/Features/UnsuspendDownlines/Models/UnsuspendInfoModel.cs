namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    public class UnsuspendMultipleModel
    {
        public UnsuspendMultipleModel() { }

        public UnsuspendMultipleModel(byte roleId, int affectedDownlines, int unchangedDownlines)
        {
            RoleId = roleId;
            AffectedDownlines = affectedDownlines;
            UnchangedDownlines = unchangedDownlines;
        }

        public byte RoleId { get; private set; }

        public int AffectedDownlines { get; private set; }

        public int UnchangedDownlines { get; private set; }
    }
}
