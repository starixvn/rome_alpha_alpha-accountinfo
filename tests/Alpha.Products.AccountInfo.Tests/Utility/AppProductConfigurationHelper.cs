namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using System.Collections.Generic;
    using Alpha.Products.Core.Configuration;

    public static class AppProductConfigurationHelper
    {
        internal static IList<IConfigurationItem> LoadApplicationProductConfiguration()
        => new List<IConfigurationItem>()
        {
                new ConfigurationItem(1, "Sportsbook", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(3, "Number Game", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(5, "Virtual Sports", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(6, "RNG Casino", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(8, "Keno", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(12, "Gold Deluxe", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(15, "ColossusBets", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(16, "Sportsbook2", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(21, "AllBet", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(22, "Macau Games", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(23, "NG2", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(24, "Fishing World", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(26, "Lucky3", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(28, "Lottery", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(30, "Bullfighting", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(31, "Ricky bullshit product", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(1000, "James 69 product", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(2000, "James 2000 product", extendProductProperty: new SearchAccountProperties(true, true, true)),
                new ConfigurationItem(3000, "3000", extendProductProperty: new SearchAccountProperties(true, true, true))
        };

        internal static IList<IConfigurationItem> LoadApplicationProductConfiguration(params int[] productIds)
        {
            var configurationItems = new List<IConfigurationItem>();

            foreach (var productId in productIds)
            {
                configurationItems.Add(new ConfigurationItem(productId, $"productName {productId}"));
            }

            return configurationItems;
        }
    }
}