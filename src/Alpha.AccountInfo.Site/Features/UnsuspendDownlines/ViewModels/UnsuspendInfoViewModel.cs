namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UnsuspendInfoViewModel : AccountInfoViewModelBase
    {
        public UnsuspendInfoViewModel(int searchAccountId, IEnumerable<UnsuspendInfoModel> unsuspendByLevel)
        {
            SearchAccountId = searchAccountId;
            UnsuspendByLevel = unsuspendByLevel;
        }

        public int SearchAccountId { get; private set; }

        public IEnumerable<UnsuspendInfoModel> UnsuspendByLevel { get; private set; }

        public int AffectedDownlinesTotal => UnsuspendByLevel.Sum(item => item.AffectedDownlines);

        public int UnchangedDownlinesTotal => UnsuspendByLevel.Sum(item => item.UnchangedDownlines);
    }
}