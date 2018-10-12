namespace Alpha.AccountInfo.Features.Shared.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        int UserId { get; }

        string IP { get; }

        string LangKey { get; }

        string Language { get; }

        bool IsInternal { get; }

        bool CanUpdateAccount { get; }

        bool AllowedAllDayBalTransfer { get; }

        bool AllowedTotalBalanceTransfer { get; }

        bool AllowTransfer { get; }

        bool CanViewReports { get; }

        bool GetDisabledProduct(int productId);

        bool GetHideProduct(int productId);

        bool GetSyncedProduct(int productId);

        bool IsHideKeno { get; }

        bool IsCasinoSync { get; }

        bool IsCasinoDisabled { get; }

        bool IsHideColossusBets { get; }

        bool DisableColossusBets { get; }

        bool IsDisabledESports { get; }

        bool IsNumberGameDisabled { get; }
    }
}