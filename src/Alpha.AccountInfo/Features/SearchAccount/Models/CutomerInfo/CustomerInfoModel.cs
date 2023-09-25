namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    using System;

    public class CustomerInfoModel
    {
        public CustomerInfoModel()
        {
        }

        public CustomerInfoModel(
            int custId,
            byte roleId,
            string userName,
            string userName2,
            string firstName,
            string lastName,
            string phone,
            string mobilePhone,
            string fax,
            bool enabled,
            bool uplineEnabled,
            bool closed,
            bool suspended,
            DateTime creatDate,
            DateTime lastLogged,
            string lastLoginIP,
            DateTime pwdExpiry,
            int memberId,
            string memberName,
            int agentId,
            string agentName,
            int masterId,
            string masterName,
            int superId,
            string superName,
            byte locked)
        {
            CustId = custId;
            RoleId = roleId;

            UserName = userName;
            UserName2 = userName2;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            MobilePhone = mobilePhone;
            Fax = fax;

            Enabled = enabled;
            UplineEnabled = uplineEnabled;
            Closed = closed;
            Suspended = suspended;

            CreatDate = creatDate;
            LastLogged = lastLogged;
            LastLoginIP = lastLoginIP;
            PwdExpiry = pwdExpiry;

            MemberId = memberId;
            MemberName = memberName;

            AgentId = agentId;
            AgentName = agentName;

            MasterId = masterId;
            MasterName = masterName;

            SuperId = superId;
            SuperName = superName;

            Locked = locked;
        }

        public int CustId { get; private set; }

        public byte RoleId { get; private set; }

        public string UserName { get; private set; }

        public string UserName2 { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Phone { get; private set; }

        public string MobilePhone { get; private set; }

        public string Fax { get; private set; }

        public byte ClosedReason { get; set; }

        public bool Enabled { get; private set; }

        public bool UplineEnabled { get; private set; }

        public bool Closed { get; private set; }

        public bool Suspended { get; private set; }

        public DateTime CreatDate { get; private set; }

        public DateTime LastLogged { get; private set; }

        public bool AllowOpenInfoPopup { get; set; }

        public string LastLoginIP { get; set; }

        public DateTime PwdExpiry { get; private set; }

        public int MemberId { get; private set; }

        public string MemberName { get; private set; }

        public int AgentId { get; private set; }

        public string AgentName { get; private set; }

        public int MasterId { get; private set; }

        public string MasterName { get; private set; }

        public int SuperId { get; private set; }

        public string SuperName { get; private set; }

        public byte Locked { get; }
    }
}
