namespace Alpha.AccountInfo.Site.Models
{
    using AccountInfo.Models;
    using Sunplus.Agent.Model.Entities;

    public class User : IUser
    {
        private readonly IUserSession userSession;

        public User(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int Id => userSession.CustId;

        public string Name => userSession.UserName;

        public byte LevelId => userSession.RoleId;

        public int SubAccId => userSession.SubAccId;

        public string SubAccName => userSession.SubUserName;

        public int UserId => userSession.UserId;

        public string IP => userSession.UserLoginIp;

        public string LangKey => userSession.LangKey;

        public bool IsInternal => userSession.IsInternal;

        public bool CanUpdateAccount
            => !userSession.IsSubAcc || (userSession.IsSubAcc && userSession.SubPageAccess.UpdateAccount);

        public bool AllowedAllDayBalTransfer => userSession.IsAllDayBalTransfer;
         
        public bool AllowedTotalBalanceTransfer => userSession.IsAllowedTotalBalanceTransfer;

        public bool AllowTransfer => userSession.SubPageAccess.Transfer; 

        public bool CanViewReports => userSession.SubPageAccess.ViewReports;

        public bool GetDisabledProduct(int productId) => userSession.GetDisabledProduct(productId);

        public bool GetHideProduct(int productId) => userSession.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => userSession.GetSyncedProduct(productId);

        public bool IsHideKeno => userSession.IsHideKeno;

        public bool IsCasinoSync => userSession.IsCasinoSync;

        public bool IsCasinoDisabled => userSession.IsCasinoDisabled;

        public bool IsHideColossusBets => userSession.IsHideColossusBets;
        public bool DisableColossusBets => userSession.DisableColossusBets;

        public bool IsDisabledESports => userSession.IsDisabledESports;

        public bool IsNumberGameDisabled => userSession.IsNumberGameDisabled;

    }
}