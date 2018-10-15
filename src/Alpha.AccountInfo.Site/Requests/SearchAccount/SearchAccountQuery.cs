namespace Alpha.AccountInfo.Site.Requests.SearchAccount
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.ViewModels.SearchAccount;
    using MediatR;

    public class SearchAccountQuery : BaseRequest, IRequest<AccountInfoViewModel>
    {
        public SearchAccountQuery(IUser viewer) : base(viewer)
        {
            ActorRoleId = viewer.LevelId;
            ActorName = viewer.Name;
            Language = viewer.Language;
        }

        public string SearchAccountName { get; set; }

        public byte ActorRoleId { get; }

        public string ActorName { get; }

        public string Language { get; }

        public bool MobileView { get; set; }

        public override bool IsValid() => true;
    }
}