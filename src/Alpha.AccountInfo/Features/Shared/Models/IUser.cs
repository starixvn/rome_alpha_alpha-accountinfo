namespace Alpha.AccountInfo.Features.Shared.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        byte LevelId { get; }

        bool IsSubAcc { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        int UserId { get; }

        string IP { get; }

        string LangKey { get; }

        string Language { get; }

        bool IsInternal { get; }

        bool CanUpdateAccount { get; }

        bool CanCreateAccount { get; }

        bool AllowedAllDayBalTransfer { get; }

        bool AllowedTotalBalanceTransfer { get; }

        bool AllowTransfer { get; }

        bool CanViewReports { get; }

        bool CanViewLog { get; }

        bool ViewMemberInfo { get; }

        bool GetDisabledProduct(int productId);

        bool GetHideProduct(int productId);

        bool GetSyncedProduct(int productId);

        bool IsHideKeno { get; }

        bool IsCasinoSync { get; }

        bool IsCasinoDisabled { get; }

        bool IsDisabledESports { get; }

        bool IsNumberGameDisabled { get; }

        bool IsHideGoldDeluxe { get; }

        bool DisableGoldDeluxe { get; }

        bool IsEnabledCustomerDateLimit { get; }

        string Site { get; }

        int Srecommend { get; }

        int Mrecommend { get; }

        int Arecommend { get; }

        bool IsAdminCanViewVNIP { get; }
    }
}