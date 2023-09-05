using Alpha.AccountInfo.Features.SearchAccount.Params;
using Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands;

namespace Alpha.MemberInfo.Features.CustomerList.Criteria
{
    public class UpdateUnlockedCommand : UpdateCustomerStatusCommand
    {
        public UpdateUnlockedCommand(UpdateCustomerStatusParam param) : base( param.SearchAccountId, param.ActorId, param.SubAccountId, param.Language, param.IP, param.StatusName)
        {
        }
    }
}