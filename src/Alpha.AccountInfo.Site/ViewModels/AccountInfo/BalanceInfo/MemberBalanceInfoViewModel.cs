namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;
    using System;
    using System.Resources;

    public class MemberBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MemberBalanceInfoModel balanceInfoModel;

        public MemberBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel,
            Func<string, string, string> getMicroSiteUrl = null,
            Func<string, ResourceManager, string> convertResourcesJson = null) 
            : base(message, customerInfoModel, getMicroSiteUrl, convertResourcesJson)
        {
            this.balanceInfoModel = (MemberBalanceInfoModel)balanceInfoModel;
        }

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.Yesterday_CustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.CustomerOutstanding);

        public decimal NetPosition => RoundNumber(balanceInfoModel.NetPosition);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public decimal BetCredit => RoundNumber(GivenCredit + balanceInfoModel.CustomerCash - Outstanding + NetPosition);

        public string BetListLink => ShowReportLink ?
            $@"{BetListsUrl}RunningBetList/Index?CustID={balanceInfoModel.CustId}&amp;CustName=ACGUUS3838033&amp;GetAll=True" 
            : string.Empty;

        public string GivenCreditLimit => $"<={MemMaxCredit_Max}";

        internal override bool ShowTransferLink => ActorLevel.IsAgentLevel() && base.ShowTransferLink;

        protected override string BuildTransferLink(decimal transferValue) =>
           $"{OldMainUrl}_Transfer/Agent/MemberTransfer.aspx?custid={balanceInfoModel.CustId}&CallBack=false";

        private decimal MemMaxCredit_Max => balanceInfoModel.MemMaxCredit_Max;

        private UserLevel ActorLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId);
    }
}