namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

    public class UpdateDisableAutoMPTStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDisableAutoMPTStatusCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName,
            bool statusValue) : base(searchAccountId, actorId, subAccountId, language, ip, statusName)
        {
            DisableAutoMPT = statusValue;
        }

        public bool DisableAutoMPT { get; }
    }
}