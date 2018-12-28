namespace Alpha.AccountInfo.Services.ProductStatus
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.AccountInfo.Constants;
    using Alpha.AccountInfo.Criteria.ProductStatus;
    using Alpha.AccountInfo.Models.ProductStatus;
    using Fanex.Data.Repository;

    public interface IProductProfileService
    {
        int GetSettingLevel(int productId);
    }

    public class ProductProfileService : IProductProfileService
    {
        private static IEnumerable<ProductProfile> NonDCSProductSetting = new List<ProductProfile>
        {
            new ProductProfile(ProductIdConstants.RNGCasino, ProductSettingLevel.Product),
            new ProductProfile(ProductIdConstants.Keno, ProductSettingLevel.BetType),
            new ProductProfile(ProductIdConstants.NumberGame, ProductSettingLevel.Product),
            new ProductProfile(ProductIdConstants.GoldDeluxe, ProductSettingLevel.Product),
            new ProductProfile(ProductIdConstants.ColossusBets, ProductSettingLevel.Product)
        };

        private static IEnumerable<ProductProfile> DCSProductSetting = new List<ProductProfile>
        {
            new ProductProfile(ProductIdConstants.VirtualSports, ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.Sportbook2,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.Allbet,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.MacauGames,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.NG2,ProductSettingLevel.Product,11),
            new ProductProfile(ProductIdConstants.FishingWorld,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.Lucky3,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.Lottery,ProductSettingLevel.Product,13),
            new ProductProfile(ProductIdConstants.Bullfighting,ProductSettingLevel.Product,13),
             new ProductProfile(ProductIdConstants.PokerGames, ProductSettingLevel.Product,13),
        };

        private readonly IDynamicRepository dynamicRepository;

        public ProductProfileService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public int GetSettingLevel(int productId)
        {
            var isNonDscProduct = NonDCSProductSetting.Any(i => i.ProductId == productId);
            if (isNonDscProduct)
            {
                return NonDCSProductSetting.First(i => i.ProductId == productId).SettingLevel;
            }

            var productProfile = dynamicRepository.Get<ProductProfile>(
                new SelectProductProfileCriteria(
                    productId,
                    DCSProductSetting.First(i => i.ProductId == productId).SettingType.Value));

            return productProfile.SettingLevel;
        }
    }
}
