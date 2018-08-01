namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Site.Helpers;

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