namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data;

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
            Disable = disable;
        }

        [SpParam(SqlParamName = "DisabledCS")]
        public bool Disable { get; }

        public bool SyncCS { get; } = true;

        public string Language { get; }

        public override string GetSettingKey() => "Acc_UpdCSStatus_Cust";
    }
}
