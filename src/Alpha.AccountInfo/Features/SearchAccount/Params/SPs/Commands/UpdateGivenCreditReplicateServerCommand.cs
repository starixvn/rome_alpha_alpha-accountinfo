namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class UpdateGivenCreditReplicateServerCommand : NonQueryCommand
    {
        public UpdateGivenCreditReplicateServerCommand(int searchAccountId, decimal creditAmount)
        {
            SearchAccountId = searchAccountId;
            CreditAmount = creditAmount;
        }

        [SpParam(SqlParamName = "CustId")]
        public int SearchAccountId { get; }

        [SpParam(SqlParamName = "NewCredit")]
        public decimal CreditAmount { get; }

        public override string GetSettingKey() => "Acc_CustomerUpdCreditOnReplicateServer";

        public override bool IsValid() => true;
    }
}