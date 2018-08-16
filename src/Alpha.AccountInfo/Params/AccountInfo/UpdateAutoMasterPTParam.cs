namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateAutoMasterPTParam : UpdateCustomerStatusParam
    {
        public UpdateAutoMasterPTParam(
            int searchAccountId,
            int actorId,
            bool disableAutoMasterPT,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DisableAutoMasterPT = disableAutoMasterPT;
        }

        public bool DisableAutoMasterPT { get; }
    }
}
