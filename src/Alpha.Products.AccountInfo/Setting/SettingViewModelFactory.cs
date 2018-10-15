namespace Alpha.Products.AccountInfo.Setting
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Alpha.Products.AccountInfo.Setting.ViewModels;
    using Alpha.Products.Core.Configuration;
    using Alpha.Products.Core.ViewModels;
    using ViewModels.Base;
    using ViewModels.Products;
    using static Alpha.Products.Core.Shared.ProductConstants;

    // TODO: simple first, better later
    public class SettingViewModelFactory : GenericViewModelFactory<ISearchAccountInfoRequest, ISettingItemViewModel>
    {
        private const int LastProductSettingItemIndex = 10000;

        public SettingViewModelFactory(ConfigurationService configurationService = null)
            : base(configurationService)
        {
        }

        protected override IEnumerable<ISettingItemViewModel> GetNonProductViewModels(ISearchAccountInfoRequest request)
        {
            yield return new EditInfoViewModel(request);
            yield return new TransferViewModel(request);
            yield return new BetSettingViewModel(request);
            yield return new CommissionViewModel(request);
            yield return new SportsbookMinPTViewModel(request);
            yield return new ResetPasswordViewModel(request, displayOrder: LastProductSettingItemIndex + 1);
            yield return new ResetSecurityCodeViewModel(request, displayOrder: LastProductSettingItemIndex + 2);
            yield return new RemoveOtpViewModel(request, displayOrder: LastProductSettingItemIndex + 3);
        }

        protected override ISettingItemViewModel GetProductViewModel(IConfigurationItem productItem, ISearchAccountInfoRequest request)
        {
            switch (productItem.ProductId)
            {
                case Sportsbook:
                    return new SportsbookViewModel(request);

                case RNGCasino:
                    return new RNGCasinoViewModel(request);

                case Keno:
                    return new KenoViewModel(request);

                case GoldDeluxe:
                    return new GoldDeluxeViewModel(request);

                case ColossusBets:
                    return new ColossusBetsViewModel(request);

                default:
                    return new DynamicProductViewModel(request, productItem.ProductId);
            }
        }
    }
}