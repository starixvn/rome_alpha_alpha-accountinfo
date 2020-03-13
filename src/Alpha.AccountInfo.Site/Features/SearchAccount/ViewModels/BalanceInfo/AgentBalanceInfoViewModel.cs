namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

    public class AgentBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly AgentBalanceInfoModel balanceInfoModel;

        public AgentBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfoModel,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, creditInfo, getMicroSiteUrl)
        {
            this.balanceInfoModel = (AgentBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundUpNumber(balanceInfoModel.AgentCash);

        public decimal YesterdayCash => RoundUpNumber(balanceInfoModel.YesterdayAgentCash);

        public override decimal TotalBalance => RoundUpNumber(balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundUpNumber(balanceInfoModel.YesterdayAgentCash + balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundUpNumber(balanceInfoModel.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundUpNumber(creditInfo.MemberMaxCredit);

        public decimal UsedCredit => RoundUpNumber(creditInfo.AgentUsedCredit);

        public override decimal GivenCredit => RoundUpNumber(creditInfo.Credit);

        public bool ShowMemberMaxCreditLimit => ActorLevel.IsMasterLevel();

        public string MemberMaxCreditLimit => $">={MemMaxCreditMin} {Resources.And} <={GivenCredit}";

        public override decimal GivenCreditMin => creditInfo.AgentCreditMin;

        public override decimal GivenCreditMax => creditInfo.AgentCreditMax;

        internal override bool ShowTransferLink => ActorLevel.IsMasterLevel() && base.ShowTransferLink;

        private decimal MemMaxCreditMin => creditInfo.MemMaxCreditMin;
    }
}