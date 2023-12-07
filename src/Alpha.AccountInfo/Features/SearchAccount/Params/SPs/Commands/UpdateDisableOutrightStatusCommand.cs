namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

    public class UpdateDisableOutrightStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDisableOutrightStatusCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName,
            bool statusValue) : base(searchAccountId, actorId, subAccountId, language, ip, statusName)
        {
            DisableOutright = statusValue;
        }

        public bool DisableOutright { get; }
    }
}