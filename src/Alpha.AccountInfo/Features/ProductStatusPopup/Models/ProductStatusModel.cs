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
            bool isDisabledUpline,
            bool isHidden,
            int? groupId)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsDisabled = isDisabled;
            IsDisabledUpline = isDisabledUpline;
            IsHidden = isHidden;
            GroupId = groupId;
        }

        public int ProductId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDisabledUpline { get; set; }

        public string Name { get; set; }

        public bool IsHidden { get; set; }

        public int? GroupId { get; set; }
    }
}