namespace Alpha.AccountInfo.Site.Features.Shared.Models
{
    using System;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Sunplus.Agent.Model.Entities;

    public class User : IUser
    {
        private const string CustomerDateLimitSessionKey = "CustomerDateLimit";
#pragma warning disable CS0618 // Type or member is obsolete
        private readonly IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete

#pragma warning disable CS0618 // Type or member is obsolete
        public User(IUserSession userSession)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            this.userSession = userSession;

        }

        public int Id => userSession.CustId;

        public string Name => userSession.UserName;

        public byte LevelId => userSession.RoleId;

        public bool IsSubAcc => userSession.IsSubAcc;

        public int SubAccId => userSession.SubAccId;

        public string SubAccName => userSession.SubUserName;

        public int UserId => userSession.UserId;

        public string IP => userSession.UserLoginIp;

        public string LangKey => userSession.LangKey;

        public string Language => userSession.Language;

        public bool IsInternal => userSession.IsInternal;

        public bool CanUpdateAccount
            => !userSession.IsSubAcc || (userSession.IsSubAcc && userSession.SubPageAccess.UpdateAccount);

        public bool AllowedAllDayBalTransfer => userSession.IsAllDayBalTransfer;

        public bool AllowedTotalBalanceTransfer => userSession.IsAllowedTotalBalanceTransfer;

        public bool AllowTransfer => userSession.SubPageAccess.Transfer;

        public bool ViewMemberInfo => userSession.SubPageAccess.ViewMemberInfo;

        public bool CanViewReports => userSession.SubPageAccess.ViewReports;

        public bool CanViewLog => userSession.SubPageAccess.ViewLog;

        public bool CanCreateAccount => userSession.SubPageAccess.CreateAccount;

        public bool GetDisabledProduct(int productId) => userSession.GetDisabledProduct(productId);

        public bool GetHideProduct(int productId) => userSession.GetHideProduct(productId);

        public bool GetSyncedProduct(int productId) => userSession.GetSyncedProduct(productId);

        public bool IsHideKeno => userSession.IsHideKeno;

        public bool IsDisabledESports => userSession.IsDisabledESports;

        public bool IsNumberGameDisabled => userSession.IsNumberGameDisabled;

        public string Site => userSession.Site;

        public bool IsEnabledCustomerDateLimit
           => userSession.GetProperty<DateTime>(CustomerDateLimitSessionKey) != default(DateTime);

        public int Srecommend => userSession.SRecommendId;

        public int Mrecommend => userSession.MRecommendId;

        public int Arecommend => userSession.ARecommendId;

        public bool IsAdminCanViewVNIP => userSession.IsAdminCanViewVNIP;

        public bool IsOfferProtectedMode => userSession.IsOfferProtectedMode;

        public int ServerId => userSession.ServerId;
    }
}