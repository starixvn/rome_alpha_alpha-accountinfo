namespace Alpha.AccountInfo.Features.Shared.Services
{
    using System.Threading;
    using Fanex.RemotingRef;

    public interface CacheRefService
    {
        string GetBetTypeName(int betTypeId);

        string GetProductName(int productId);

        string GetProductName(int productId, string languageCode);

        string GetSportTypeName(int sportTypeId);
    }

    public class CacheRefServiceImpl : CacheRefService
    {
        private readonly IRemotingRefService remotingRefService;

        public CacheRefServiceImpl(IRemotingRefService remotingRefService)
        {
            this.remotingRefService = remotingRefService;
        }

        public string GetBetTypeName(int betTypeId)
            => remotingRefService.GetBetTypeName(betTypeId.ToString(), LanguageCode);

        public string GetProductName(int productId)
            => GetProductName(productId, LanguageCode);

        public string GetProductName(int productId, string languageCode)
            => remotingRefService.GetResourceDataByRefIdAndResourceId(
                "Product",
                productId.ToString(),
                languageCode,
                string.Empty);

        public string GetSportTypeName(int sportTypeId)
            => remotingRefService.GetSportName(sportTypeId.ToString(), LanguageCode, null) ?? remotingRefService.GetResourceDataByRefIdAndResourceId(
                "DummySport",
                sportTypeId.ToString(),
                LanguageCode,
                string.Empty);

        private static string LanguageCode
            => Thread.CurrentThread.CurrentCulture.Name;
    }
}
