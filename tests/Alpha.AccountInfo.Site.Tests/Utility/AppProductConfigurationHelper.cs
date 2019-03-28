namespace Alpha.AccountInfo.Tests.Utility
{
    using Alpha.Products.AccountInfo;
    using Alpha.Products.Core.Configuration;
    using System.Collections.Generic;

    public static class AppProductConfigurationHelper
    {
        public static IList<IConfigurationItem> LoadApplicationProductConfiguration()
        => new List<IConfigurationItem>()
        {
                new ConfigurationItem(1, "Sportsbook", extendProductProperty: new SearchAccountProperties(true, true, true))
        };

        public static IList<IConfigurationItem> LoadApplicationProductConfiguration(params int[] productIds)
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