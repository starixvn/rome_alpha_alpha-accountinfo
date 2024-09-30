namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using System;
    using Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo;
    using Alpha.AccountInfo.Features.SearchAccount.Models.BalanceInfo;
    using Alpha.AccountInfo.Site.Features.SearchAccount.Requests;

    public class MasterBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MasterBalanceInfoModel balanceInfoModel;

        public MasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            CustomerInfoModel customerInfoModel,
            CreditInfo creditInfo,
            IBalanceInfoModel balanceInfoModel,
            bool isAllDayBalanceInfo,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, creditInfo, isAllDayBalanceInfo, getMicroSiteUrl)
        {
            this.balanceInfoModel = (MasterBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundUpNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundUpNumber(balanceInfoModel.YesterdayMasterCash);

        public override decimal TotalBalance
            => RoundUpNumber(
                balanceInfoModel.MasterCash +
                balanceInfoModel.AgentCash +
                balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance
            => RoundUpNumber(
                balanceInfoModel.YesterdayMasterCash +
                balanceInfoModel.YesterdayAgentCash +
                balanceInfoModel.YesterdayCustomerCash);

        public override decimal Outstanding => RoundUpNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundUpNumber(creditInfo.MasterUsedCredit);

        public override decimal GivenCredit => RoundUpNumber(creditInfo.Credit);

        public override decimal GivenCreditMin => creditInfo.MasterCreditMin;

        public override decimal GivenCreditMax => creditInfo.MasterCreditMax;

        public override decimal PendingTransfer3rd => balanceInfoModel.PendingTransfer3rd;

        public override decimal RewardReceivable => RoundUpNumber(balanceInfoModel.RewardReceivable ?? 0);

        public decimal RewardPayable => RoundUpNumber(balanceInfoModel.RewardPayable ?? 0);

        public override string RewardCurrency => balanceInfoModel.RewardCurrency;
    }
}