namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class SearchAccountQuery : BaseRequest, IRequest<AccountInfoViewModel>
    {
        private readonly IUser viewer ;

        public SearchAccountQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public byte ActorRoleId => viewer.LevelId;

        public string ActorName => viewer.Name;

        public string Language => viewer.Language;

        public bool MobileView { get; set; }

        public bool IsEnabledCustomerDateLimit => viewer.IsEnabledCustomerDateLimit;

        public override bool IsValid() => viewer.ViewMemberInfo;
    }
}