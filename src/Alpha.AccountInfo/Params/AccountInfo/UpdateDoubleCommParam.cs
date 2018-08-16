namespace Alpha.AccountInfo.Params.ProductStatus
{
    public class UpdateDoubleCommParam : UpdateCustomerStatusParam
    {
        public UpdateDoubleCommParam(
            int searchAccountId,
            int actorId,
            bool doubleComm,
            int subAccountId,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DoubleComm = doubleComm;
        }

        public bool DoubleComm { get; }
    }
}
