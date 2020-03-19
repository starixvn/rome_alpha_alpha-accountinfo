namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands
{
    using System.Collections;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class UpdateAllDayBalanceTransferCommand : NonQueryCommand
    {
        public UpdateAllDayBalanceTransferCommand(
            int searchAccountId,
            int actorId,
            string language,
            string ip,
            string statusName,
            bool statusValue)
        {
            SearchAccountId = searchAccountId;
            ActorId = actorId;
            Language = language;
            IP = ip;
            StatusName = statusName;
            StatusValue = statusValue;
        }

        

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        public string Language { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public string StatusName { get; }

        [SpParam(Ignored = true)]
        public bool StatusValue { get; }

        public override Hashtable GetSpParams()
        {
            var spParams = base.GetSpParams();

            spParams.Add(StatusName, StatusValue);

            return spParams;
        }

        public override string GetSettingKey()
            => "Acc_CustomerUpdAllDayBalTransfer";

        public override bool IsValid()
            => SearchAccountId > 0
            && ActorId > 0;
    }
}