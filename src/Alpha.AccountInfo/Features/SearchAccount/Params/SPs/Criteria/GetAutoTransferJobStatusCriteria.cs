namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Criteria
{
    using Fanex.Data.Repository;

    public class GetAutoTransferJobStatusCriteria : NonQueryCommand
    {
        public GetAutoTransferJobStatusCriteria()
        {
            FromCustId = 0;
            ToCustId = 0;
            IsAllowAutoTransfer = true;
            ErrCode = 0;
            ErrMsg = string.Empty;
        }

        public int FromCustId { get; }

        public int ToCustId { get; }

        public bool IsAllowAutoTransfer { get; }

        public bool IsAutoJobRunning { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "SiteDB_Age_ClearBalanceCust_Job_StatusCheck";

        public override bool IsValid() => true;
    }
}