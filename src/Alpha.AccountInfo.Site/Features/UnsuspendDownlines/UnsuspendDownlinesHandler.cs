namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using MediatR;

    public class UnsuspendDownlinesHandler : 
        IAsyncRequestHandler<UnsuspendViewListRequest, UnsuspendListViewModel>,
        IAsyncRequestHandler<UnsuspendSingleRequest, UnsuspendInfoViewModel>,
        IAsyncRequestHandler<UnsuspendMultipleRequest, IEnumerable<UnsuspendMultipleByUserModel>>
    {
        private readonly UnsuspendDownlinesService unsuspendDownlinesService;

        public UnsuspendDownlinesHandler(UnsuspendDownlinesService unsuspendDownlinesService)
        {
            this.unsuspendDownlinesService = unsuspendDownlinesService;
        }

        public async Task<UnsuspendInfoViewModel> Handle(UnsuspendSingleRequest message)
            => new UnsuspendInfoViewModel(
                message.SearchAccountId,
                await unsuspendDownlinesService.GetUnsuspendInfoModel(message.SearchAccountId)
                .ConfigureAwait(false));

        public async Task<UnsuspendListViewModel> Handle(UnsuspendViewListRequest message)
             => new UnsuspendListViewModel(
                 message.SearchAccountId,
                 await unsuspendDownlinesService.GetUnsuspendViewListModel().ConfigureAwait(false));

        public async Task<IEnumerable<UnsuspendMultipleByUserModel>> Handle(UnsuspendMultipleRequest message)
            => await unsuspendDownlinesService.GetUnsuspendMultipleModel().ConfigureAwait(false);
    }
}