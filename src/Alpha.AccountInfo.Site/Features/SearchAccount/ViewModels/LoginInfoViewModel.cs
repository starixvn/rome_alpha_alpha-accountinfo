namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
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
            LastLogin = DateTime.MinValue == lastLogin ? string.Empty : lastLogin.ToString();
            LastTxn = DateTime.MinValue == lastTxn ? string.Empty : lastTxn.ToString();
            LoginIP = loginIP;
        }

        public string CreatedDate { get; }

        public string LastLogin { get; }

        public string LastTxn { get; }

        public string LoginIP { get; }
    }
}