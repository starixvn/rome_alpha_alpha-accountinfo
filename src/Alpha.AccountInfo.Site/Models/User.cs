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

        public string IP => userSession.UserLoginIp;

        public bool IsInternal => userSession.IsInternal;

        public bool CanUpdateAccount => userSession.SubPageAccess.UpdateAccount;
    }
}