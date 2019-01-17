namespace Alpha.AccountInfo.Models.ProductStatus
{
    public class ProductProfile
    {
        public ProductProfile()
        {

        }

        public ProductProfile(int productId, byte settingLevel, int? settingType = null)
        {
            ProductId = productId;
            SettingLevel = settingLevel;
            SettingType = settingType;
        }

        public int ProductId { get; }

        public byte SettingLevel { get; }

        public int? SettingType { get; }
    }
}
