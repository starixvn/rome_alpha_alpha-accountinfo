namespace Alpha.AccountInfo.Site.ViewModels.SearchAccount
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
            CreatedDate = createdDate.ToString();
            LastLogin = lastLogin.ToString();
            LastTxn = lastTxn.ToString();
            LoginIP = loginIP;
        }

        public string CreatedDate { get; }

        public string LastLogin { get; }

        public string LastTxn { get; }

        public string LoginIP { get; }
    }
}