namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using MediatR;
    using System.Collections.Generic;

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