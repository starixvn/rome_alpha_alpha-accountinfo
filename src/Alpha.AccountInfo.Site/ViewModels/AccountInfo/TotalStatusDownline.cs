namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public class TotalStatusDownline
    {
        public TotalStatusDownline(
            int totalActive,
            int totalClosed,
            int totalSuspend)
        {
            TotalActive = totalActive;
            TotalClosed = totalClosed;
            TotalSuspend = totalSuspend;
        }

        public int TotalActive { get; }

        public int TotalClosed { get; }

        public int TotalSuspend { get; }
    }
}