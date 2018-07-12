namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public class AccountInfoViewModel
    {
        public AccountInfoViewModel(
             CustomerInfoViewModel customerInfo,
             BalanceInfoViewModel balanceInfo,
             DownlineListViewModel downlineList,
             StatusViewModel status,
             SettingViewModel setting,
             LoginInfoViewModel loginInfo,
             OnlineListViewModel onlineList)
        {
            CustomerInfo = customerInfo;
            BalanceInfo = balanceInfo;
            DownlineList = downlineList;
            Status = status;
            Setting = setting;
            LoginInfo = loginInfo;
            OnlineList = onlineList;
        }

        public CustomerInfoViewModel CustomerInfo { get; }

        public BalanceInfoViewModel BalanceInfo { get; }

        public DownlineListViewModel DownlineList { get; }

        public StatusViewModel Status { get; }

        public SettingViewModel Setting { get; }

        public LoginInfoViewModel LoginInfo { get; }

        public OnlineListViewModel OnlineList { get; }
    }
}