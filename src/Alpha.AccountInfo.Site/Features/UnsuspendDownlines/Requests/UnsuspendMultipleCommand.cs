namespace Alpha.AccountInfo.Site.Features.UnsuspendDownlines.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System.Collections.Generic;

    public class UnsuspendCustomerInfo
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public bool Suspended { get; set; }
    } 

    public class UnsuspendMultipleCommand : BaseRequest, IRequest<UnsuspendMultipleResultViewModel>
    {
        private readonly IUser viewer;

        public UnsuspendMultipleCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public IEnumerable<UnsuspendCustomerInfo> UnsuspendList { get; set; }

        public string ActorName => viewer.Name;

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid() => viewer.CanUpdateAccount && !viewer.IsInternal;
    }
}