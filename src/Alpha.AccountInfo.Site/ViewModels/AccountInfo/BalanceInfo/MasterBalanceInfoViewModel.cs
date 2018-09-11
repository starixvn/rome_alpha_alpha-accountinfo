namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using System;

    public class MasterBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly MasterBalanceInfoModel balanceInfoModel;

        public MasterBalanceInfoViewModel(
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel,
            Func<string, string, string> getMicroSiteUrl = null)
            : base(message, customerInfoModel, getMicroSiteUrl)
        {
            this.balanceInfoModel = (MasterBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundNumber(balanceInfoModel.MasterCash);

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.Yesterday_MasterCash);

        public override decimal TotalBalance
            => RoundNumber(balanceInfoModel.MasterCash + balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance 
            => RoundNumber(balanceInfoModel.Yesterday_MasterCash + balanceInfoModel.Yesterday_AgentCash + balanceInfoModel.Yesterday_CustomerCash);

        public override decimal Outstanding => RoundNumber(balanceInfoModel.MasterPositionTaking);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.MasterUsedCredit);

        public string GivenCreditLimit => $">={MasterCredit_Min} {Resources.And} <={MasterCredit_Max}";

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        private decimal MasterCredit_Min => balanceInfoModel.MasterCredit_Min;

        private decimal MasterCredit_Max => balanceInfoModel.MasterCredit_Max;
    }
}