namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class UpdateAllDayBalanceTransferCommand : NonQueryCommand
    {
        public UpdateAllDayBalanceTransferCommand(
            int searchAccountId,
            int actorId,
            string language,
            string ip,
            bool statusValue)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            Language = language;
            IP = ip;
            IsAllDayBalTransfer = statusValue.ToString().ToLowerInvariant();
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        public string Language { get; }

        public string IP { get; }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public string IsAllDayBalTransfer { get; }

        public override string GetSettingKey()
            => "Acc_CustomerUpdAllDayBalTransfer";

        public override bool IsValid()
            => SearchAccountId > 0
            && ActorId > 0;
    }
}