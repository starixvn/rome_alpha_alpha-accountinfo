namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public abstract class NonQueryProductStatusCriteria : NonQueryCommand
    {
        protected NonQueryProductStatusCriteria(
            int custId,
            int subAccId,
            int actorId,
            string ip)
        {
            CustId = custId;
            SubAccId = subAccId;
            ActorId = actorId;
            IP = ip;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        public string IP { get; }

        public byte SystemId { get; } = 2;

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override bool IsValid()
            => CustId > 0
            && SubAccId >= 0
            && ActorId > 0;
    }
}
