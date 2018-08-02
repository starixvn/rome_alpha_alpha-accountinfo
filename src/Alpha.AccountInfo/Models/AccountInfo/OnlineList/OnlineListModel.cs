namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class OnlineListModel
    {
        public OnlineListModel()
        {
        }

        public OnlineListModel(string ip, string reload_time)
        {
            IP = ip;
            Reload_Time = reload_time;
        }

        public string IP { get; }

        public string Reload_Time { get; }

    }
}
