namespace Alpha.AccountInfo.Configurations
{
    using System.Configuration;

    public static class AppSettings
    {
        public static bool IsEnabled(string settingKey)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings[settingKey])
                || ConfigurationManager.AppSettings[settingKey] == "0")
            {
                return false;
            }

            return true;
        }

        public static string AssetAppPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AssetAppPath"];
            }
        }

        public static string OldMainAppPath
        {
            get
            {
                return ConfigurationManager.AppSettings["OldMainAppPath"];
            }
        }
    }
}