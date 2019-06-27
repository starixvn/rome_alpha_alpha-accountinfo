namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    public class UnsuspendSingleModel
    {
        public UnsuspendSingleModel() { }

        public UnsuspendSingleModel(byte roleId, int affectedDownlines, int unchangedDownlines)
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
