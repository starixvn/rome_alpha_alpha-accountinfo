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
             double givenCredit,
             double currentGivenCredit,
             double newGivenCredit,
             double difference)
        {
            TotalCash = totalCash;
            YesterdayCash = yesterdayCash;

            TotalBalance = totalBalance;
            YesterdayBalance = yesterdayBalance;

            Outstanding = outstanding;
            UsedCredit = usedCredit;
            GivenCredit = givenCredit;
            CurrentGivenCredit = currentGivenCredit;
            NewGivenCredit = newGivenCredit;
            Difference = difference;
        }

        public double TotalCash { get; }

        public double YesterdayCash { get; }

        public double TotalBalance { get; }

        public double YesterdayBalance { get; }

        public double Outstanding { get; }

        public double UsedCredit { get; }

        public double GivenCredit { get; }

        public double CurrentGivenCredit { get; }

        public double NewGivenCredit { get; }

        public double Difference { get; }
    }
}