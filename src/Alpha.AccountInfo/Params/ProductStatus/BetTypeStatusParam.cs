namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class BetTypeStatusParam
    {
        public BetTypeStatusParam()
        {
        }

        public BetTypeStatusParam(int sportType, int betType, bool isDisabled)
        {
            SportType = sportType;
            BetType = betType;
            IsDisabled = isDisabled;
        }

        public int SportType { get; }

        public int BetType { get; }

        public bool IsDisabled { get; }
    }
}
