namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class OnlineUserModel
    {
        public OnlineUserModel(string ip, string loginTime)
        {
            IP = ip;
            LoginTime = loginTime;
        }

        public string IP { get; }

        public string LoginTime { get; }
    }
}
