using System.Collections.Generic;

namespace Alpha.AccountInfo.Features.ProductStatusPopup
{
    public static class SportTypeConstants
    {
        public const int AllBet = 211;
        public const int SexyGaming = 243;
        public const int SAGaming = 244;

        public static readonly IEnumerable<int> SportTypeOfLiveCasino = new List<int>
        {
            SportTypeConstants.AllBet,
            SportTypeConstants.SexyGaming,
            SportTypeConstants.SexyGaming
        };
    }
}
