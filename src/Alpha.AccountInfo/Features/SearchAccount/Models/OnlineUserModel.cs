namespace Alpha.AccountInfo.Features.SearchAccount.Models
{
    using System;

    public class OnlineUserModel
    {
        public OnlineUserModel()
        {
        }

        public OnlineUserModel(string ip, DateTime loginTime)
        {
            IP = ip;
            LoginTime = loginTime;
        }

        public string IP { get; set; }

        public DateTime LoginTime { get; private set; }

        public string FormattedLoginTime => LoginTime.ToString("dd MMM hh:mm tt");
    }
}