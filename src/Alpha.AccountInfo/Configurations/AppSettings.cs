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

        public static string SiteGroup => GetConfigValue("SiteGroup");

        public static bool IsIBCIndo => SiteGroup.Equals("IBCIndo", StringComparison.OrdinalIgnoreCase);

        public static string GetConfigValue(string configName) => AppSettingValues[configName];

        public static byte SettingStatus {
            get
            {
                byte.TryParse(GetConfigValue("SettingStatus"), out byte settingStatus);
                return settingStatus;
            }
        }

        public static bool IsDisabledRecheckin => GetConfigValue("DisabledCheckDuplicationLogin") == "1";

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