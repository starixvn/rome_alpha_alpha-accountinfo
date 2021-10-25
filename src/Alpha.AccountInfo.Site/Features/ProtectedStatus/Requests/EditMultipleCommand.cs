namespace Alpha.AccountInfo.Site.Features.ProtectedStatus.Requests
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.ProtectedStatus.Models;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class EditMultipleCommand : BaseRequest, IRequest<IEnumerable<Result>>
    {
        public EditMultipleCommand(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.IsSubAcc ? viewer.SubAccName : viewer.Name;
            SubAccId = viewer.SubAccId;
            LangKey = viewer.LangKey;
            IP = viewer.IP;
            IsInternal = viewer.IsInternal;
            ServerId = viewer.ServerId;
            CanUpdateAccount = viewer.CanUpdateAccount;
        }

        public CustomerProtectedStatus[] ProtectedStatus { get; set; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public bool IsInternal { get; }

        public int ServerId { get; }

        private bool CanUpdateAccount { get; }


        public override bool IsValid()
            => CanUpdateAccount && !IsInternal && ProtectedStatus?.Length > 0;
    }
}