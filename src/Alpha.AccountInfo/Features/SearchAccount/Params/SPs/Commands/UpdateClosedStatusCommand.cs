namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

    public class UpdateClosedStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateClosedStatusCommand(
            int searchAccountId,
            int actorId,
            int subAccountId,
            string language,
            string ip,
            string statusName,
            bool statusValue) : base(searchAccountId, actorId, subAccountId, language, ip, statusName)
        {           
            Closed = statusValue;
            SystemID = 2;
        }

        public bool Closed { get; }

        public int SystemID { get; }
    }
}