namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands
{
    using Fanex.Data.Repository;

    public sealed class TurnOffAutoTransferCriteria : NonQueryCommand
    {
        public int UplineId { get; set; }

        public int Creator { get; set; }

        public bool IsAllDownline { get; set; }

        public string IP { get; set; }

        public string TargetIds { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "Age_ClearBalanceCust_SettingUpdate_Downline";
        }

        public override bool IsValid()
        {
            return UplineId > 0;
        }
    }
}