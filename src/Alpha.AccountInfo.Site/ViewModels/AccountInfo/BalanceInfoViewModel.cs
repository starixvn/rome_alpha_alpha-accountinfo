namespace Alpha.AccountInfo.Site.ViewModels.AccountInfo
{
    public class BalanceInfoViewModel
    {
        public BalanceInfoViewModel(
             double totalCash,
             double yesterdayCash,
             double totalBalance,
             double yesterdayBalance,
             double outstanding,
             double usedCredit,
             double givenCredit)
        {
            TotalCash = totalCash;
            YesterdayCash = yesterdayCash;

            TotalBalance = totalBalance;
            YesterdayBalance = yesterdayBalance;

            Outstanding = outstanding;
            UsedCredit = usedCredit;
            GivenCredit = givenCredit;
        }

        public double TotalCash { get; }

        public double YesterdayCash { get; }

        public double TotalBalance { get; }

        public double YesterdayBalance { get; }

        public double Outstanding { get; }

        public double UsedCredit { get; }

        public double GivenCredit { get; }
    }
}