namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using System;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class GetLastTxnQuery : BaseRequest, IRequest<string>
    {
        public GetLastTxnQuery(IUser viewer) : base(viewer)
        {
            UplineId = viewer.Id;
            IsInternal = viewer.IsInternal;
        }

        public string Username { get; set; }

        public int UplineId { get; }

        public bool IsInternal { get; }

        public override bool IsValid()
            => IsValidName(Username) && UplineId > 0;
    }
}