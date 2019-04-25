namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using MediatR;

    public class UnsuspendSingleRequest : SearchAccountQueryBase, IRequest<UnsuspendSingleViewModel>
    {
        public UnsuspendSingleRequest(IUser viewer) : base(viewer)
        {
            Language = viewer.Language;
        }

        public string Language { get; private set; }

    }
}