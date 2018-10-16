namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class UpdateKenoStatusCriteria : NonQueryProductStatusCriteria
    {
#pragma warning disable S107 // Methods should not have too many parameters

        public UpdateKenoStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            string language,
            int betType,
            bool disable,
            int uplineId) : base(custId, subAccId, userId, ip)
#pragma warning restore S107 // Methods should not have too many parameters
        {
            Language = language;
            BetType = betType;
            Disable = disable;
            UplineId = uplineId;
        }

        public int BetType { get; }

        public bool Disable { get; }

        public int UplineId { get; }

        public string Language { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdDisableKeno";

        public override bool IsValid()
            => base.IsValid() && BetType > 0 && UplineId > 0;
    }
}