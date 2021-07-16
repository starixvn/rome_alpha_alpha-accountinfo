namespace Alpha.AccountInfo.Site.Features.OpenDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using MediatR;
    using SearchAccount.ViewModels;
    using Shared;
    using System.Collections.Generic;

    public class OpenCustomerInfo
    {
        public int SearchAccountId { get; set; }

        public string Username { get; set; }

        public bool IsClosed { get; set; }
    }

    public class OpenMultipleCommand : BaseRequest, IRequest<OpenMultipleResultViewModel>
    {
        public OpenMultipleCommand(IUser viewer) : base(viewer)
        {
            ActorName = viewer.Name;
            ActorId = viewer.Id;
            SubAccountId = viewer.SubAccId;
            Language = viewer.LangKey;
            IP = viewer.IP;
            IsInternal = viewer.IsInternal;
            CanUpdateAccount = viewer.CanUpdateAccount;
        }

        public IEnumerable<OpenCustomerInfo> OpenList { get; set; }

        public int RoleId { get; set; }

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