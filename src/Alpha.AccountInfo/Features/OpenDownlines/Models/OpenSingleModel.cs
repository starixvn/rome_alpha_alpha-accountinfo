namespace Alpha.AccountInfo.Features.OpenDownlines.Models
{
    public class OpenSingleModel
    {
        public OpenSingleModel()
        {
        }

        public OpenSingleModel(byte roleId, int affectedDownlines, int unchangedDownlines)
        {
            RoleId = roleId;
            AffectedDownlines = affectedDownlines;
            UnchangedDownlines = unchangedDownlines;
        }

        public byte RoleId { get; }

        public int AffectedDownlines { get; }

        public int UnchangedDownlines { get; }
    }
}