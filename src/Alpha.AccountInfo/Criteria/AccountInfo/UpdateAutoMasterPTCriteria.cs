namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    using Fanex.Data;

    public class UpdateAutoMasterPTCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateAutoMasterPTCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool disableAutoMasterPT,
            string language,
            string ip): base(searchAccountId, actorId, subAccountId, language, ip)
        {
            DisableAutoMasterPT = disableAutoMasterPT;
        }

        [SpParam(SqlParamName = "disableautompt")]
        public bool DisableAutoMasterPT { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdMaster_DisableAutoMPT";
    }
}
