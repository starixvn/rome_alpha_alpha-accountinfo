namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

    public class AgentBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly AgentBalanceInfoModel balanceInfoModel;

        public AgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, getMicroSiteUrl)
        {
            this.balanceInfoModel = (AgentBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundDownNumber(balanceInfoModel.AgentCash);

        public decimal YesterdayCash => RoundDownNumber(balanceInfoModel.YesterdayAgentCash);

        public override decimal TotalBalance => RoundDownNumber(balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundDownNumber(balanceInfoModel.YesterdayAgentCash + balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundDownNumber(balanceInfoModel.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundDownNumber(balanceInfoModel.MemberMaxCredit);

        public decimal UsedCredit => RoundDownNumber(balanceInfoModel.AgentUsedCredit);

        public decimal GivenCredit => RoundDownNumber(balanceInfoModel.Credit);

        public bool ShowMemberMaxCreditLimit => ActorLevel.IsMasterLevel();

        public string MemberMaxCreditLimit => $">={MemMaxCreditMin} {Resources.And} <={GivenCredit}";

        public decimal GivenCreditMin => balanceInfoModel.AgentCreditMin;

        public decimal GivenCreditMax => balanceInfoModel.AgentCreditMax;

        internal override bool ShowTransferLink => ActorLevel.IsMasterLevel() && base.ShowTransferLink;

        private decimal MemMaxCreditMin => balanceInfoModel.MemMaxCreditMin;  
    }
}