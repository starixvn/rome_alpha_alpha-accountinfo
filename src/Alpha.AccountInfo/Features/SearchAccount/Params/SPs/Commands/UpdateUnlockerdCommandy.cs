namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

    public class UpdateUnlockedCommand : UpdateCustomerStatusCommand
    {
        public UpdateUnlockedCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName) : base(searchAccountId, actorId, subAccountId, language, ip, statusName)
        {
        }
    }
}