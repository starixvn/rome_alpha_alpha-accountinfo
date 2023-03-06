namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using System;
    using System.Web.Mvc;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        public CustomerInfoQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.Name;
            ActorRoleId = viewer.LevelId;
            AllowedTotalBalanceTransfer = viewer.AllowedTotalBalanceTransfer;
            AllowTransfer = viewer.AllowTransfer;
            CanViewReports = viewer.CanViewReports;
            CanViewLog = viewer.CanViewLog;
            CanUpdateAccount = viewer.CanUpdateAccount;
            CanCreateAccount = viewer.CanCreateAccount;
            CanViewMemberInfo = viewer.ViewMemberInfo;
            IsAdminCanViewVNIP = viewer.IsAdminCanViewVNIP;
        }

        [AllowHtml]
        public string SearchAccountName { get; set; }

        public int ActorId { get; }

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public bool AllowedTotalBalanceTransfer { get; }

        public bool AllowTransfer { get; }

        public bool CanViewReports { get; }

        public bool CanViewLog { get; }

        public bool CanUpdateAccount { get; }

        public bool CanCreateAccount { get; }

        public bool CanViewMemberInfo { get; }

        public bool IsAdminCanViewVNIP { get; }

        public override bool IsValid() =>
            IsValidName(SearchAccountName) &&
            CanViewMemberInfo &&
            !SearchAccountName.Trim().Equals(ActorName, StringComparison.OrdinalIgnoreCase);
    }
}