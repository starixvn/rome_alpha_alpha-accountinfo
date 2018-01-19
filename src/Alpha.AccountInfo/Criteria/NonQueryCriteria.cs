namespace Alpha.AccountInfo.Criteria
{
    using Fanex.Data.Repository;

    public abstract class NonQueryCriteria : CriteriaBase
    {
        protected NonQueryCriteria(
            int subAccId,
            int userId,
            string language,
            string ip)
        {
            SubAccId = subAccId;
            UserId = userId;
            Language = language;
            IP = ip;
        }

        public int SubAccId { get; }

        public int UserId { get; }

        public string Language { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override bool IsValid()
        {
            return SubAccId >= 0 && UserId > 0 && !string.IsNullOrEmpty(Language) && !string.IsNullOrEmpty(IP);
        }
    }
}
