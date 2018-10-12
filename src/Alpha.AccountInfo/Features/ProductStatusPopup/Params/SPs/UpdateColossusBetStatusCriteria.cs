namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class UpdateColossusBetStatusCriteria : NonQueryProductStatusCriteria
    {
        public UpdateColossusBetStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            string language,
            bool disable) : base(custId, subAccId, userId, ip)
        {
            Language = language;
            Disable = disable;
        }

        public bool Disable { get; }

        public string Language { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdDisableColossusBet";
    }
}
