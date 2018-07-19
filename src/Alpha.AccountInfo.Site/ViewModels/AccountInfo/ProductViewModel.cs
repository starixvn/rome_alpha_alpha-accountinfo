namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public class ProductViewModel
    {
        public ProductViewModel(StatusViewModel status, SettingViewModel setting)
        {
            Status = status;
            Setting = setting;
        }

        public StatusViewModel Status { get; }

        public SettingViewModel Setting { get; }
    }
}