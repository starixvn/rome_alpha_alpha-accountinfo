namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class UpdateClosedCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateClosedCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool closed,
            bool disabledCasino,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            Closed = closed;
            DisabledCasino = disabledCasino;
        }

        public bool Closed { get; }

        public bool DisabledCasino { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdClosed";
    }
}
