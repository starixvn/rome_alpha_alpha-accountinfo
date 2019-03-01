namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels;
    using Alpha.AccountInfo.Site.Features.Shared;
    using MediatR;
    using System;

    public class CustomerInfoQuery : BaseRequest, IRequest<CustomerInfoViewModel>
    {
        private readonly IUser viewer;

        public CustomerInfoQuery(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public string SearchAccountName { get; set; }

        public int ActorId => viewer.Id;

        public byte ActorRoleId => viewer.LevelId;

        public bool AllDayBalTransfer => viewer.AllowedAllDayBalTransfer;

        public bool AllowedTotalBalanceTransfer => viewer.AllowedTotalBalanceTransfer;

        public bool AllowTransfer => viewer.AllowTransfer;

        public bool CanViewReports => viewer.CanViewReports;

        public bool CanViewLog => viewer.CanViewLog;

        public bool CanUpdateAccount => viewer.CanUpdateAccount;

        public override bool IsValid() =>
            IsValidName(SearchAccountName) &&
            !SearchAccountName.Trim().Equals(viewer.Name, StringComparison.OrdinalIgnoreCase);
    }
}