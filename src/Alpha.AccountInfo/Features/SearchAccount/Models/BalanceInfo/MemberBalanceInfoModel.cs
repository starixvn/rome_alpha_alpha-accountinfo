namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class MemberBalanceInfoModel : IBalanceInfoModel
    {
        public MemberBalanceInfoModel(
             MemberBalanceInfoDTO memberBalanceInfoDTO,
             decimal balance,
             decimal customerOutstanding)
        {
            Balance = balance;
            CustomerOutstanding = customerOutstanding;

            BetCredit = Credit + balance;
            CustomerCash = balance + customerOutstanding;
            YesterdayCustomerCash = memberBalanceInfoDTO.YesterdayCustomerCash;
        }

        public int CustId { get; private set; }

        public decimal Balance { get; private set; }
        
        public decimal Credit { get; private set; }


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