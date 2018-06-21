namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System;

    public class LoginInfoViewModel
    {
        public LoginInfoViewModel(
            DateTime createdDate,
            DateTime lastLogin,
            DateTime lastTxn,
            string loginIP)
        {
            CreatedDate = createdDate;
            LastLogin = lastLogin;
            LastTxn = lastTxn;
            LoginIP = loginIP;
        }

        public DateTime CreatedDate { get; }

        public DateTime LastLogin { get; }

        public DateTime LastTxn { get; }

        public string LoginIP { get; }
    }
}