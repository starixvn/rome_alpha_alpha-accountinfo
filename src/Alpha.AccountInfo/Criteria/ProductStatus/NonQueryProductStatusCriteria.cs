namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Configurations;
    using Fanex.Data.Repository;

    public abstract class NonQueryProductStatusCriteria : NonQueryCommand
    {
        protected NonQueryProductStatusCriteria(
            int custId,
            int subAccId,
            int userId,
            string ip)
        {
            CustId = custId;
            SubAccId = subAccId;
            UserId = userId;
            IP = ip;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public int UserId { get; }

        public string IP { get; }

        public byte SystemId { get; } = AppSettings.SystemId;

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override bool IsValid() => CustId > 0 && SubAccId >= 0 && UserId > 0 && !string.IsNullOrEmpty(IP);

        public virtual string ParseErrMsg()
        {
            return ErrMsg;
        }
    }
}
