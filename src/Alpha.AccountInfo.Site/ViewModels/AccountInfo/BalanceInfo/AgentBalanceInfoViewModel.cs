namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.Shared.AppResources;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;

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

        public decimal TotalCash => RoundNumber(balanceInfoModel.AgentCash);

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.YesterdayAgentCash);

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.YesterdayAgentCash + balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundNumber(balanceInfoModel.MemberMaxCredit);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.AgentUsedCredit);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public string GivenCreditLimit => $">={AgentCredit_Min} {Resources.And} <={AgentCredit_Max}";

        public bool ShowMemberMaxCreditLimit => ActorLevel.IsMasterLevel();

        public string MemberMaxCreditLimit => $">={MemMaxCredit_Min} {Resources.And} <={GivenCredit}";

        internal override bool ShowTransferLink => ActorLevel.IsMasterLevel() && base.ShowTransferLink;

        private decimal MemMaxCredit_Min => balanceInfoModel.MemMaxCreditMin;

        private decimal AgentCredit_Min => balanceInfoModel.AgentCreditMin;

        private decimal AgentCredit_Max => balanceInfoModel.AgentCreditMax;
    }
}