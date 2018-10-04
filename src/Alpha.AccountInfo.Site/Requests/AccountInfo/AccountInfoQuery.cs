namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class AccountInfoQuery : BaseRequest, IRequest<AccountInfoViewModel>
    {
        public AccountInfoQuery(IUser viewer) : base(viewer)
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