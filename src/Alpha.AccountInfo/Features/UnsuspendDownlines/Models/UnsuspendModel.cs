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

    public class UnsuspendModel
    {
        public UnsuspendModel() { }

        public UnsuspendModel(IEnumerable<UnsuspendByLevelModel> unsuspendByLevel)
        {
            UnsuspendByLevel = unsuspendByLevel;
        }

        public IEnumerable<UnsuspendByLevelModel> UnsuspendByLevel { get; private set; }

        public int AffectedDownlinesTotal => UnsuspendByLevel.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => UnsuspendByLevel.Sum(item => item.UnchangedDownlines);
    }
}
