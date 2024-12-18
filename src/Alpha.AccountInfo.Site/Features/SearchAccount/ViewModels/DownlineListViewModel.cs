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

        public int TotalPages => model.TotalPages;
    }
}