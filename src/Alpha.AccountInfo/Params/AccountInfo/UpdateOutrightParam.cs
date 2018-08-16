namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateOutrightParam : UpdateCustomerStatusParam
    {
        public UpdateOutrightParam(
            int searchAccountId,
            int actorId,
            bool disableOutright,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DisableOutright = disableOutright;
        }

        public bool DisableOutright { get; }
    }
}
