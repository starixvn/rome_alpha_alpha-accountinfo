namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class UnsuspendByLevelModel
    {
        public UnsuspendByLevelModel() { }

        public UnsuspendByLevelModel(byte roleId, int affectedDownlines, int unchangedDownlines)
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
