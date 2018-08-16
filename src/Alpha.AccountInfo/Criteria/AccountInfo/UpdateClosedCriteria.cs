namespace Alpha.AccountInfo.Criteria.AccountInfo
{
    public class UpdateClosedCriteria : UpdateCustomerStatusCriteria
    {
        public UpdateClosedCriteria(
            int searchAccountId,
            int actorId,
            int subAccountId,
            bool closed,
            string language,
            string ip) : base(searchAccountId, actorId, subAccountId, language, ip)
        {
            Closed = closed;
        }

        public bool Closed { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdClosed";
    }
}
