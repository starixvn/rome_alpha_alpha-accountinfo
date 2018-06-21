namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class CutomerAllInfoModel
    {
        public CutomerAllInfoModel(
             CustomerInfoModel customerInfo,
             LineInfoModel lineInfo,
             CustomerBalanceInfoModel customerCashInfo)
        {
            CustomerInfo = customerInfo;
            LineInfo = lineInfo;
            CustomerCashInfo = customerCashInfo;
        }

        public CustomerInfoModel CustomerInfo { get; }

        public LineInfoModel LineInfo { get; }

        public CustomerBalanceInfoModel CustomerCashInfo { get; }
    }
}
