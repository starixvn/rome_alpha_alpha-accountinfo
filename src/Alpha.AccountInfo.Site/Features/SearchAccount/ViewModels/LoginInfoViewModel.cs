namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;

    public class LoginInfoViewModel
    {
       public LoginInfoViewModel(
            DateTime createdDate,
            DateTime lastLogin,
            string loginIP)
        {
            CreatedDate = createdDate.ToString();
            LastLogin = DateTime.MinValue == lastLogin ? string.Empty : lastLogin.ToString();
            LoginIP = loginIP;
        }

        public string CreatedDate { get; }

        public string LastLogin { get; }

        public string LoginIP { get; }
    }
}