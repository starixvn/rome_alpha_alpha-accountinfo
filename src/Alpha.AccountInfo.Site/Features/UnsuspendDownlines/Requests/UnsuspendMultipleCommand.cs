namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Collections.Generic;

    public class UnsuspendCustomerInfo
    {
        public int SearchAccountId { get; set; }

        public string Username { get; set; }

        public bool Suspended { get; set; }
    } 

    public class UnsuspendMultipleCommand : BaseRequest, IRequest<UnsuspendMultipleResultViewModel>
    {
        public UnsuspendMultipleCommand(IUser viewer) : base(viewer)
        {
            ActorName = viewer.Name;
            ActorId = viewer.Id;
            SubAccountId = viewer.SubAccId;
            Language = viewer.LangKey;
            IP = viewer.IP;
            IsInternal = viewer.IsInternal;
            CanUpdateAccount = viewer.CanUpdateAccount;
        }

        public IEnumerable<UnsuspendCustomerInfo> UnsuspendList { get; set; }

        public string ActorName { get; }

        public int ActorId { get; }

        public int SubAccountId { get; }

        public string Language { get; }

        public string IP { get; }

        public bool IsInternal { get; }

        private bool CanUpdateAccount { get; }

        public override bool IsValid() => CanUpdateAccount && !IsInternal;
    }
}