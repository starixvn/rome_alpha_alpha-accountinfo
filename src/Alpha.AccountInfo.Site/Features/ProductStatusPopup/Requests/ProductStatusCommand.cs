namespace Alpha.AccountInfo.Site.Features.ProductStatusPopup.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using System;

    public class ProductStatusCommand : ProductStatusQuery, IRequest<Result>
    {
        public ProductStatusCommand(IUser viewer) : this(viewer, null)
        {
        }

        public ProductStatusCommand(IUser viewer, Func<string, bool> checkSiteGroup)
            : base(viewer, checkSiteGroup)
        {
            ActorId = viewer.UserId;
            ActorName = viewer.Name;
            SubAccId = viewer.SubAccId;
            LangKey = viewer.LangKey;
            Language = viewer.Language;
            IP = viewer.IP;
        }

        public BetTypeStatus[] BetTypeStatus { get; set; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public string Language { get; }

        public override bool IsValid()
            => base.IsValid() && BetTypeStatus?.Length > 0;
    }
}