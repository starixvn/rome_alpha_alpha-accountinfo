namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class AccountInfoQuery : BaseRequest, IRequest<AccountInfoViewModel>
    {
        public AccountInfoQuery(IUser viewer)
            : base(viewer)
        {
            ActorRoleId = viewer.LevelId;
            Language = viewer.Language;
        }
        public byte ActorRoleId { get; }

        public string Language { get; }

        public override bool IsValid() => ActorRoleId > 0;
    }
}