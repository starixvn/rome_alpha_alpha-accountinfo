namespace Alpha.AccountInfo.Site.Requests.SearchAccount
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;

    public class UserNameSuggestionQuery : BaseRequest, IRequest<IEnumerable<UsernameSuggestion>>
    {
        private readonly IUser viewer;

        public UserNameSuggestionQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SuggestionQuery { get; set; }

        public int ActorId => viewer.Id;

        public bool IsInternal => viewer.IsInternal;

        public override bool IsValid() => IsValidName(SuggestionQuery);
    }
}