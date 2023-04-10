namespace Alpha.AccountInfo.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;

        public static NameValueCollection AppSettingValues
        {
            get { return appSettingValues ?? ConfigurationManager.AppSettings; }

            set { appSettingValues = value; }
        }

        public static string StoredProcedurePath => GetConfigValue("StoredProcedurePath");

        public static string RemoteRefURL => GetConfigValue("RemoteRefURL");

        public static string ReportDomain => GetConfigValue("Report:Domain") ?? string.Empty;

        public static string BetlistDomain => GetConfigValue("Betlist:Domain") ?? string.Empty;

        public static bool IsStandaloneReports => !string.IsNullOrWhiteSpace(ReportDomain);

        public static string GetConfigValue(string configName) => AppSettingValues[configName];

        public static byte SettingStatus {
            get
            {
                byte.TryParse(GetConfigValue("SettingStatus"), out byte settingStatus);
                return settingStatus;
            }
        }

        public static bool IsDisabledRecheckin => GetConfigValue("DisabledCheckDuplicationLogin") == "1";

        public static string JsVersion => GetConfigValue("JsVersion");

        public static string SiteLabel => GetConfigValue("SiteLabel");

        public static string SiteLabel789y => GetConfigValue("SiteLabel789y");

        public static bool SiteLabelIsNot789y => !SiteLabel.Equals(SiteLabel789y, StringComparison.OrdinalIgnoreCase);

        public static void Verify()
        {
            Require(StoredProcedurePath, nameof(StoredProcedurePath));
        }

        private static void Require(string key, string keyName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ConfigurationErrorsException($"{keyName} is null or whitespace");
            }
        }
    }
}