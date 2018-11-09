namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.AccountInfo.Features.SearchAccount.Models;

    public class DownlineListViewModel
    {
        private readonly DownlineListModel model;

        public DownlineListViewModel(DownlineListModel model, IEnumerable<TotalDownlinesSummary> totalDownlines)
        {
            this.model = model;
            TotalDownlines = totalDownlines.OrderByDescending(item => item.RoleId);
        }

        public IEnumerable<DownlineDto> DownlineList => model.Downlines;

        public IEnumerable<TotalDownlinesSummary> TotalDownlines { get;}

        public int TotalPages => model.TotalPages; 
    }
}