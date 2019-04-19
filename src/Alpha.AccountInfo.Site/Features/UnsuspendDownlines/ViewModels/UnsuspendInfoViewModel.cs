namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UnsuspendInfoViewModel
    {
        public UnsuspendInfoViewModel() { }

        public UnsuspendInfoViewModel(IEnumerable<UnsuspendByLevelModel> unsuspendByLevel)
        {
            UnsuspendByLevel = unsuspendByLevel;
        }

        public IEnumerable<UnsuspendByLevelModel> UnsuspendByLevel { get; private set; }

        public int AffectedDownlinesTotal => UnsuspendByLevel.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => UnsuspendByLevel.Sum(item => item.UnchangedDownlines);
    }
}