namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    public class UpdateCasinoStatusCriteria : NonQueryProductStatusCriteria
    {
        public UpdateCasinoStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip,
            string language,
            bool disable) : base(custId, subAccId, userId, ip)
        {
            Language = language;
            DisabledCS = disable;
        }

        public bool DisabledCS { get; }

        public bool SyncCS { get; } = true;

        public string Language { get; }

        public override string GetSettingKey() => "Acc_UpdCSStatus_Cust";

        public override bool IsValid() => base.IsValid() && !string.IsNullOrEmpty(Language);
    }
}
