
using Fanex.Data;
using Alpha.AccountInfo.Configurations;

namespace Alpha.AccountInfo.Site.Utilities
{
    public static class DbSettingProviderManagerExtension
    {   
        public static DbSettingProviderManager UseShardDbSettingProvider(this DbSettingProviderManager dbSettingProviderManager)
            => dbSettingProviderManager.Use(new ShardDbSettingProvider(new ShardInfoAccessor(),
                    resourcePath: AppSettings.StoredProcedurePath));

    }
}