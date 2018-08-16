namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class OutrightParam
    {
        public OutrightParam()
        {
        }

        public OutrightParam(
            int custId,
            int actorId,
            bool disableOutright,
            int subAccountId,
            string language,
            string ip)
        {
            CustId = custId;
            ActorId = actorId;
            SubAccountId = subAccountId;
            DisableOutright = disableOutright;
            Language = language;
            IP = ip;
        }

        public int CustId { get; }

        public int ActorId { get; }

        public int SubAccountId { get; }

        public bool DisableOutright { get; }

        public string Language { get; }

        public string IP { get; }
    }
}
