namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System;

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

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.Yesterday_AgentCash);

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.Yesterday_AgentCash + balanceInfoModel.Yesterday_CustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundNumber(balanceInfoModel.MemberMaxCredit);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.AgentUsedCredit);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public string GivenCreditLimit => $">={AgentCredit_Min} {Resources.And} <={AgentCredit_Max}";

        public string MemberMaxCreditLimit 
            => ActorLevel.IsMasterLevel() ? $">={MemMaxCredit_Min} {Resources.And} <={GivenCredit}" : string.Empty;

        internal override bool ShowTransferLink => ActorLevel.IsMasterLevel() && base.ShowTransferLink;

        private decimal MemMaxCredit_Min => balanceInfoModel.MemMaxCredit_Min;

        private decimal AgentCredit_Min => balanceInfoModel.AgentCredit_Min;

        private decimal AgentCredit_Max => balanceInfoModel.AgentCredit_Max;
    }
}