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
            bool isSync,
            bool isHidden,
            bool isDisabledUpline,
            bool isHiddenUpline,
            bool isSyncedUpline)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsDisabled = isDisabled;
            IsSync = isSync;
            IsHidden = isHidden;
            IsDisabledUpline = isDisabledUpline;
            IsHiddenUpline = isHiddenUpline;
            IsSyncedUpline = isSyncedUpline;
        }

        public int ProductId { get; }

        public int SportType { get; }

        public int BetType { get; }

        public bool IsDisabled { get; }

        public bool IsSync { get; }

        public bool IsHidden { get; }

        public bool IsDisabledUpline { get; }

        public bool IsHiddenUpline { get; }

        public bool IsSyncedUpline { get; }

        public string BetTypeName { get; set; }
    }
}
