namespace Alpha.AccountInfo.Site.Integration
{
    using System.Collections.Generic;
    using Alpha.Products.Core.Configuration;

    internal static class ProductConfiguration
    {
        /// <summary>
        /// it will be replaced by code loading setting from shared config
        /// </summary>
        /// <returns></returns>
        internal static IList<IConfigurationItem> LoadApplicationProductConfiguration()
        => new List<IConfigurationItem>()
        {
                new ConfigurationItem(1, "Sportsbook", true),
                new ConfigurationItem(2, "Racing", false),
                new ConfigurationItem(3, "Number Game"),
                new ConfigurationItem(4, "Live Casino", false),
                new ConfigurationItem(5, "Virtual Sports"),
                new ConfigurationItem(6, "RNG Casino"),
                new ConfigurationItem(8, "Keno"),
                new ConfigurationItem(12, "Gold Deluxe", true),
                new ConfigurationItem(15, "ColossusBets", false),
                new ConfigurationItem(16, "Sportsbook2"),
                new ConfigurationItem(21, "AllBet"),
                new ConfigurationItem(22, "Macau Games"),
                new ConfigurationItem(23, "NG2"),
                new ConfigurationItem(24, "Fishing World"),
                new ConfigurationItem(26, "Lucky3"),
                new ConfigurationItem(28, "Lottery"),
                new ConfigurationItem(30, "Bullfighting"),
                new ConfigurationItem(31, "Ricky bullshit product"),
                new ConfigurationItem(32, "James 69 product")
        };
    }
}