namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class UpdateSuspendedCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateSuspendedCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool suspended,
            bool disabledCasino,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            Suspended = suspended;
            DisabledCasino = disabledCasino;
        }

        public bool Suspended { get; }

        public bool DisabledCasino { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdSuspended";
    }
}
