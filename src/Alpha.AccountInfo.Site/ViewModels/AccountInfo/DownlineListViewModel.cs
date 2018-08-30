namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models.AccountInfo;
    using System.Collections.Generic;

    public class DownlineListViewModel
    {
        private readonly DownlineListModel model;

        public DownlineListViewModel(DownlineListModel model)
        {
            this.model = model;
        }

        public IEnumerable<string> DownlineList => model.Downlines;

        public TotalStatusDownline TotalStatusAgent
            => new TotalStatusDownline(model.TotalAgActive, model.TotalAgClosed, model.TotalAgSuspended);

        public TotalStatusDownline TotalStatusMember
             => new TotalStatusDownline(model.TotalMemActive, model.TotalMemClosed, model.TotalMemSuspended);
    }
}