namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class UpdateKenoStatusCriteria : NonQueryCriteria
    {
        public UpdateKenoStatusCriteria(
            int custId,
            int betType,
            bool disable,
            int uplineId,
            int subAccId,
            int userId,
            string language,
            string ip) : base(subAccId, userId, language, ip)
        {
            CustId = custId;
            BetType = betType;
            Disable = disable;
            UplineId = uplineId;
        }

        public int CustId { get; }

        public int BetType { get; }

        public bool Disable { get; }

        public int UplineId { get; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdDisableKeno";
        }

        public override bool IsValid()
        {
            return base.IsValid() && CustId > 0 && BetType > 0 && UplineId > 0;
        }
    }
}
