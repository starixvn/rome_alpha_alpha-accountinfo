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
            CustId = memberBalanceInfoDTO.CustId;
            Credit = memberBalanceInfoDTO.Credit;

            CustomerCash = balance + customerOutstanding;
            YesterdayCustomerCash = memberBalanceInfoDTO.YesterdayCustomerCash;

            CustomerOutstanding = customerOutstanding;
            NetPosition = netPosition;
            MemMaxCreditMax = memberBalanceInfoDTO.MemMaxCreditMax;

            BetCredit = Credit + balance + netPosition;
        }

        public int CustId { get; private set; }

        public decimal Credit { get; private set; }

        public decimal BetCredit { get; private set; }

        public decimal NetPosition { get; private set; }

        public decimal MemMaxCreditMax { get; private set; }

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
            Credit = credit;
            YesterdayCustomerCash = yesterdayCustomerCash;
            MemMaxCreditMax = memMaxCreditMax;
        }

        public int CustId { get; private set; }

        public decimal Credit { get; private set; }

        public decimal MemMaxCreditMax { get; private set; }

        public decimal YesterdayCustomerCash { get; private set; }
    }
}
