namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models;

    public class DownlineListViewModel
    {
        private readonly DownlineListModel model;

        public DownlineListViewModel(DownlineListModel model)
        {
            this.model = model;
        }

        public IEnumerable<DownlineDto> DownlineList => model.Downlines;

        public TotalStatusDownline TotalStatusAgent
            => new TotalStatusDownline(
                model.TotalAgActive,
                model.TotalAgClosed,
                model.TotalAgSuspended);

        public TotalStatusDownline TotalStatusMember
             => new TotalStatusDownline(
                 model.TotalMemActive,
                 model.TotalMemClosed,
                 model.TotalMemSuspended);

        public int TotalPages => 5; //model.TotalPages;
    }
}