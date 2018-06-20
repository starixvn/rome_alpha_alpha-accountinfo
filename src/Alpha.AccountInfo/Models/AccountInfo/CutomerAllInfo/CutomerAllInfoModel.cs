namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class CutomerAllInfoModel
    {
        public CutomerAllInfoModel(
             CustomerInfoModel customerInfo,
             LineInfoModel lineInfo,
             CustomerCashInfoModel customerCashInfo)
        {
            CustomerInfo = customerInfo;
            LineInfo = lineInfo;
            CustomerCashInfo = customerCashInfo;
        }

        public CustomerInfoModel CustomerInfo { get; }

        public LineInfoModel LineInfo { get; }

        public CustomerCashInfoModel CustomerCashInfo { get; }
    }
}
