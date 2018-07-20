namespace Alpha.AccountInfo.Models.AccountInfo
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
         
         bool closedReason,
         bool enabled,
         bool uplineEnabled,
         bool closed,
         bool suspended,
         DateTime createDate,
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
        string superName)
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

            ClosedReason = closedReason;
            Enabled = enabled;
            UplineEnabled = uplineEnabled;
            Closed = closed;
            Suspended = suspended;
            
            CreateDate = createDate;
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
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public string UserName { get; }

        public string UserName2 { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Phone { get; }

        public string MobilePhone { get; }

        public string Fax { get; }

        public bool ClosedReason { get; }

        public bool Enabled { get; }

        public bool UplineEnabled { get; }

        public bool Closed { get; }

        public bool Suspended { get; }

        public DateTime CreateDate { get; }

        public DateTime LastLogged { get; }

        public string LastLoginIP { get; }

        public DateTime PwdExpiry { get; }

        public int MemberId { get; }

        public string MemberName { get; }

        public int AgentId { get; }

        public string AgentName { get; }

        public int MasterId { get; }

        public string MasterName { get; }

        public int SuperId { get; }

        public string SuperName { get; }
    }
}
