namespace Alpha.AccountInfo.Features.SearchAccount.Models
{
    public class OnlineUserModel
    {
        public OnlineUserModel() { }

        public OnlineUserModel(string ip, string loginTime)
        {
            IP = ip;
            LoginTime = loginTime;
        }

        public string IP { get; private set; }

        public string LoginTime { get; private set; }
    }
}
