namespace Alpha.AccountInfo.Models.AccountInfo
{
    using System.Collections.Generic;

    public class DownlineListModel
    {
        public DownlineListModel(
         IEnumerable<DownlineInfoModel> downlines,
         int totalAgActive,
         int totalAgClosed,
         int totalAgSuspended,
         int totalMemActive,
         int totalMemClosed,
         int totalMemSuspended)
        {
            Downlines = downlines;

            TotalAgActive = totalAgActive;
            TotalAgClosed = totalAgClosed;
            TotalAgSuspended = totalAgSuspended;

            TotalMemActive = totalMemActive;
            TotalMemClosed = totalMemClosed;
            TotalMemSuspended = totalMemSuspended;
        }

        public IEnumerable<DownlineInfoModel> Downlines{ get; }

        public int TotalAgActive { get; }

        public int TotalAgClosed { get; }

        public int TotalAgSuspended { get; }

        public int TotalMemActive { get; }

        public int TotalMemClosed { get; }

        public int TotalMemSuspended { get; }
    }
}
