namespace Alpha.AccountInfo.Site.Features.OpenDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using SearchAccount.Requests;
    using SearchAccount.ViewModels;

    public class OpenSingleRequest : SearchAccountQueryBase, IRequest<OpenSingleViewModel>
    {
        public OpenSingleRequest(IUser viewer) : base(viewer)
        {
            Language = viewer.Language;
        }

        public string SearchAccountName { get; set; }

        public string Language { get; }
    }
}