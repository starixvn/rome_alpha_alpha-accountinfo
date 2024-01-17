namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

    public class UpdateDisableProtectedStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateDisableProtectedStatusCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName,
            bool statusValue) : base(searchAccountId, actorId, subAccountId, language, ip, statusName)
        {
            IsProtectionMode = statusValue;
        }

        public bool IsProtectionMode { get; }
    }
}