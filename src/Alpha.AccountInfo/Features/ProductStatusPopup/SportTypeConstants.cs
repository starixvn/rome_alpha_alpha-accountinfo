using System.Collections.Generic;

namespace Alpha.AccountInfo.Features.ProductStatusPopup
{
    public static class SportTypeConstants
    {
        public const int AllBet = 211;
        public const int BBIN = 241;
        public const int SexyGaming = 243;
        public const int SAGaming = 244;
        public const int Lottery = 220;
        public const int TableGaming = 222;

        public static readonly IEnumerable<int> SportTypeOfProduct = new List<int>
        {
            AllBet,
            BBIN,
            SexyGaming,
            SAGaming,
            Lottery,
            TableGaming
        };
    }
}