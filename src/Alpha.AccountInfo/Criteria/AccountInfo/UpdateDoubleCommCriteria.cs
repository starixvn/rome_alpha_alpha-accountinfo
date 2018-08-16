namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;

    public class UpdateDoubleCommCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateDoubleCommCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool doubleComm,
            string language,
            string ip): base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DoubleComm = doubleComm;
        }

        [SpParam(SqlParamName = "doublecommlosing")]
        public bool DoubleComm { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdDoubleCommLosing";
    }
}
