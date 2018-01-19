namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class ProductStatusParam
    {
        public ProductStatusParam()
        {
        }

        public ProductStatusParam(int sportType, int betType, bool isDisabled)
        {
            SportType = sportType;
            BetType = betType;
            IsDisabled = isDisabled;
        }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsDisabled { get; set; }
    }
}
