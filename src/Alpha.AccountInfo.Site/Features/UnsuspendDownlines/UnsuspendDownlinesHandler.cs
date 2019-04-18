namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines
{
    using System.Threading.Tasks;
    using Alpha.AccountInfo.Features.UnsuspendDownlines;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests;
    using MediatR;

    public class UnsuspendDownlinesHandler : 
        IAsyncRequestHandler<UnsuspendDownlinesRequest, UnsuspendDownlinesModel>,
        IAsyncRequestHandler<UnsuspendRequest, UnsuspendModel>
    {
        private readonly UnsuspendDownlinesService unsuspendDownlinesService;

        public UnsuspendDownlinesHandler(UnsuspendDownlinesService unsuspendDownlinesService)
        {
            this.unsuspendDownlinesService = unsuspendDownlinesService;
        }

        public async Task<UnsuspendModel> Handle(UnsuspendRequest message)
        {
            return await unsuspendDownlinesService.GetUnsuspendModel().ConfigureAwait(false);
        }

        public async Task<UnsuspendDownlinesModel> Handle(UnsuspendDownlinesRequest message)
        {
            return await unsuspendDownlinesService.GetUnsuspendDownlinesModel().ConfigureAwait(false);
        }
    }
}