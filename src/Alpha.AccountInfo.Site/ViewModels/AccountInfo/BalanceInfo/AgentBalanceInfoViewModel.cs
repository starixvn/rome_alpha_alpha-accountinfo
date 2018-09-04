namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    using Alpha.AccountInfo.AppResources;
    using Alpha.AccountInfo.Models;
    using Alpha.AccountInfo.Models.AccountInfo;
    using Alpha.AccountInfo.Site.Requests.ProductStatus;
    using Alpha.Products.Core.Shared;

    public class AgentBalanceInfoViewModel : BalanceInfoViewModelBase
    {
        private readonly AgentBalanceInfoModel balanceInfoModel;

        public AgentBalanceInfoViewModel( 
            CustomerInfoQuery message,
            IBalanceInfoModel balanceInfoModel,
            CustomerInfoModel customerInfoModel) : base(message, customerInfoModel)
        {
            this.balanceInfoModel = (AgentBalanceInfoModel)balanceInfoModel;
        }

        public decimal TotalCash => RoundNumber(balanceInfoModel.AgentCash);

        public decimal YesterdayCash => RoundNumber(balanceInfoModel.Yesterday_AgentCash);

        public override decimal TotalBalance => RoundNumber(balanceInfoModel.AgentCash + balanceInfoModel.CustomerCash);

        public override decimal YesterdayBalance => RoundNumber(balanceInfoModel.Yesterday_AgentCash + balanceInfoModel.Yesterday_CustomerCash);

        public decimal Outstanding => RoundNumber(balanceInfoModel.AgentPositionTaking);

        public decimal MemberMaxCredit => RoundNumber(balanceInfoModel.MemberMaxCredit);

        public decimal UsedCredit => RoundNumber(balanceInfoModel.AgentUsedCredit);

        public decimal GivenCredit => RoundNumber(balanceInfoModel.Credit);

        public string GivenCreditLimit => $">={AgentCredit_Min} {Resources.And} <={AgentCredit_Max}";

        public string MemberMaxCreditLimit => ActorIsSuperLevel ? string.Empty : $">={MemMaxCredit_Min} {Resources.And} <={GivenCredit}";

        public string OutstandingLink => message.ViewReports && Outstanding > 0 ?
                $@"{ReportsUrl}outstanding/Agent?CustId={balanceInfoModel.CustId}&CustName={message.UserName}
                &CustLevelId=2&BreadcrumbLevelJson={BreadcrumbLevelJson}" : string.Empty;

        protected override bool ShowTransferLink => !message.AllDayBalTransfer && !ActorIsSuperLevel && message.AllowTransfer;

        private decimal MemMaxCredit_Min => balanceInfoModel.MemMaxCredit_Min;

        private decimal AgentCredit_Min => balanceInfoModel.AgentCredit_Min;

        private decimal AgentCredit_Max => balanceInfoModel.AgentCredit_Max;

        private bool ActorIsSuperLevel => Enumeration.FromValue<UserLevel>(message.ActorRoleId).IsSuperLevel();

        protected override string BuildTransferLink(decimal transferValue) =>
            $@"{OldMainUrl}_Transfer/Master/AgentTransfer.aspx?custid={balanceInfoModel.CustId}&status=1
            &CallBack=false&username={message.UserName}&isyesterday=1&&amt={transferValue}";
    }
}