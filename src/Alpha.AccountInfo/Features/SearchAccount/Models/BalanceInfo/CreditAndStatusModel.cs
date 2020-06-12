namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class CreditAndStatusModel
    {
        public CreditAndStatusModel()
        {
        }

        public CreditAndStatusModel(
            decimal credit,
            decimal creditMin,
            decimal creditMax,
            string status)
        {
            Credit = credit;
            CreditMin = creditMin;
            CreditMax = creditMax;
            Status = status;
        }

        public decimal Credit { get; }

        public decimal CreditMin { get; }

        public decimal CreditMax { get; }

        public string Status { get; }
    }
}
