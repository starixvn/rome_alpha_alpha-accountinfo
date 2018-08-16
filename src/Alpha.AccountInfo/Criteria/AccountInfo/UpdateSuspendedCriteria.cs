namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class UpdateSuspendedCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateSuspendedCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool suspended,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            Suspended = suspended;
        }

        public bool Suspended { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdSuspended";
    }
}
