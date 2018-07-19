namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class DownlineInfoModel
    {
        public DownlineInfoModel(
         string userName,
         string closedReason)
        {
            UserName = userName;
            ClosedReason = closedReason;
        }

        public string UserName { get; }

        public string ClosedReason { get; }

    }
}
