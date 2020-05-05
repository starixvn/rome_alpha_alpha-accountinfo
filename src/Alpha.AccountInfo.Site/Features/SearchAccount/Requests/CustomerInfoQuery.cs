namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System;
    using System.Web.Mvc;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        public CustomerInfoQuery(IUser viewer) : base(viewer)
        {
            ActorId = viewer.Id;
            ActorName = viewer.Name;
            ActorRoleId = viewer.LevelId;
            AllDayBalTransfer = viewer.AllowedAllDayBalTransfer;
            AllowedTotalBalanceTransfer = viewer.AllowedTotalBalanceTransfer;
            AllowTransfer = viewer.AllowTransfer;
            CanViewReports = viewer.CanViewReports;
            CanViewLog = viewer.CanViewLog;
            CanUpdateAccount = viewer.CanUpdateAccount;
            CanCreateAccount = viewer.CanCreateAccount;
            CanViewMemberInfo = viewer.ViewMemberInfo;
        }

        [AllowHtml]
        public string SearchAccountName { get; set; }

        public int ActorId { get; } 

        public string ActorName { get; }

        public byte ActorRoleId { get; }

        public bool AllDayBalTransfer { get; }

        public bool AllowedTotalBalanceTransfer { get; } 

        public bool AllowTransfer { get; }

        public bool CanViewReports { get; }

        public bool CanViewLog { get; }

        public bool CanUpdateAccount { get; }

        public bool CanCreateAccount { get; }

        public bool CanViewMemberInfo { get; }

        public override bool IsValid() =>
            IsValidName(SearchAccountName) &&
            CanViewMemberInfo &&
            !SearchAccountName.Trim().Equals(ActorName, StringComparison.OrdinalIgnoreCase);
    }
}