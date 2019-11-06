namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class MemberBalanceInfoModel : IBalanceInfoModel
    {
        public MemberBalanceInfoModel(
             MemberBalanceInfoDTO memberBalanceInfoDTO,
             decimal balance,
             decimal customerOutstanding,
             decimal netPosition)
        {
            Balance = balance;
            CustomerOutstanding = customerOutstanding;
            NetPosition = netPosition;

            CustId = memberBalanceInfoDTO.CustId;
            CustomerCash = balance + customerOutstanding;
            YesterdayCustomerCash = memberBalanceInfoDTO.YesterdayCustomerCash;
        }

        public int CustId { get; private set; }

        public decimal Balance { get; private set; }

        public decimal NetPosition { get; private set; }

        public decimal CustomerCash { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }

        public decimal CustomerOutstanding { get; private set; }
    }

    public class MemberBalanceInfoDTO
    {
        public MemberBalanceInfoDTO()
        {
        }

        public MemberBalanceInfoDTO(
             int custId,
             decimal credit,
             decimal yesterdayCustomerCash,
             decimal memMaxCreditMax)
        {
            CustId = custId;
            YesterdayCustomerCash = yesterdayCustomerCash;
        }

        public int CustId { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }
    }
}