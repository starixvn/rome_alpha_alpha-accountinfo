namespace Alpha.AccountInfo.Models.ProductStatus
{
    public class ProductStatusModel
    {
        public ProductStatusModel()
        {
        }

        public ProductStatusModel(
            int productId,
            int sportType,
            int betType,
            bool isDisabled,
            bool isDisabledUpline)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsDisabled = isDisabled;
            IsDisabledUpline = isDisabledUpline;
        }

        public int ProductId { get; }

        public int SportType { get; }

        public int BetType { get; }

        public bool IsDisabled { get; }

        public bool IsDisabledUpline { get; }

        public string BetTypeName { get; set; }
    }
}
