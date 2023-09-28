namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data.Repository;

    public class SelectLastTxnCriteria : CriteriaBase
    {
        public SelectLastTxnCriteria(string username, int uplineId, bool isInternal)
        {
            Username = username;
            UplineId = uplineId;
            IsInternalAgent = isInternal;
        }

        public string Username { get; }

        public int UplineId { get; }

        public bool IsInternalAgent { get; }

        public override string GetSettingKey()
            => "Age_CustomerSelMaxTransDate";

        public override bool IsValid()
            => !string.IsNullOrWhiteSpace(Username) && UplineId > 0;
    }
}
