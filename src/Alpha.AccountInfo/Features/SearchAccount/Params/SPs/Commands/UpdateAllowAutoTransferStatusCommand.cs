using Alpha.AccountInfo.Features.SearchAccount.Params;
using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateAllowAutoTransferStatusCommand : UpdateCustomerStatusCommand
    {
        public UpdateAllowAutoTransferStatusCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
            AllowAutoTransfer = param.StatusValue;
        }

        public bool AllowAutoTransfer { get; }

    }
}