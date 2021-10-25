namespace Alpha.AccountInfo.Site.Features.ProtectedStatus
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.ProtectedStatus;
    using Alpha.AccountInfo.Features.ProtectedStatus.Params;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.ProtectedStatus.Requests;
    using Alpha.AccountInfo.Site.Features.ProtectedStatus.ViewModels;
    using MediatR;

    public class ProtectedStatusHandler :
        IAsyncRequestHandler<EditMultipleRequest, EditMultipleViewModel>,
        IAsyncRequestHandler<EditMultipleCommand, IEnumerable<Result>>
    {
        private readonly ProtectedStatusService protectedStatusService;

        public ProtectedStatusHandler(ProtectedStatusService protectedStatusService)
        {
            this.protectedStatusService = protectedStatusService;
        }

        public async Task<EditMultipleViewModel> Handle(EditMultipleRequest message)
        {
            var protectedStatus = await protectedStatusService.GetProtectedStatusAsync(
                new GetProtectedStatusParam(message.ViewerId, message.ViewerLevelId, message.CustIds));

            return new EditMultipleViewModel(protectedStatus, message.Language);
        }

        public Task<IEnumerable<Result>> Handle(EditMultipleCommand message)
            => protectedStatusService.UpdateMultipleAsync(
                new UpdateMultipleProtectedStatusParam(
                    message.ProtectedStatus,
                    message.ActorId,
                    message.ActorName,
                    message.SubAccId,
                    message.LangKey,
                    message.IP,
                    message.IsInternal,
                    message.ServerId));
    }
}