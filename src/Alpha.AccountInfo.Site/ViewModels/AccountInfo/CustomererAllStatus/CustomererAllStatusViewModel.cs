namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public class CustomererAllStatusViewModel
    {
        public CustomererAllStatusViewModel(StatusViewModel status, SettingViewModel setting)
        {
            Status = status;
            Setting = setting;
        }

        public StatusViewModel Status { get; }

        public SettingViewModel Setting { get; }
    }
}