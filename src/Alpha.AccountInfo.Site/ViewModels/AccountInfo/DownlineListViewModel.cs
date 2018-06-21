namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System.Collections.Generic;

    public class DownlineListViewModel
    {
        public DownlineListViewModel(
            IEnumerable<string> downlineList,
            TotalStatusDownline totalStatusAgent,
            TotalStatusDownline totalStatusMember)
        {
            DownlineList = downlineList;
            TotalStatusAgent = totalStatusAgent;
            TotalStatusMember = totalStatusMember;
        }

        public IEnumerable<string> DownlineList { get; }

        public TotalStatusDownline TotalStatusAgent { get; }

        public TotalStatusDownline TotalStatusMember { get; }

        public static string BuildDownlineInfoLink(string username)
            => $"AccountInfo.aspx?username={username}";
    }
}