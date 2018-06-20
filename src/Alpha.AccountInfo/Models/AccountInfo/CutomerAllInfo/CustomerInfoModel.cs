using System;

namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class CustomerInfoModel
    {
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
         double credit,
         bool closedReason,
         bool enabled,
         bool uplineEnabled,
         bool closed,
         bool suspended,
         double netPosition,
         DateTime createDate,
         DateTime lastLogged,
         string lastLoginIP,
         DateTime pwdExpiry,
         double masterCredit_Min,
         double masterCredit_Max,
         double agentCredit_Min,
         double agentCredit_Max,
         double memMaxCredit_Min,
         double memMaxCredit_Max,
         int totalAgActive,
         int totalAgClosed,
         int totalAgSuspended,
         int totalMemActive,
         int totalMemClosed,
         int totalMemSuspended)
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

            Credit = credit;
            ClosedReason = closedReason;
            Enabled = enabled;
            UplineEnabled = uplineEnabled;
            Closed = closed;
            Suspended = suspended;
            NetPosition = netPosition;
            CreateDate = createDate;
            LastLogged = lastLogged;
            LastLoginIP = lastLoginIP;
            PwdExpiry = pwdExpiry;

            MasterCredit_Min = masterCredit_Min;
            MasterCredit_Max = masterCredit_Max;
            AgentCredit_Min = agentCredit_Min;
            AgentCredit_Max = agentCredit_Max;
            MemMaxCredit_Min = memMaxCredit_Min;
            MemMaxCredit_Max = memMaxCredit_Max;

            TotalAgActive = totalAgActive;
            TotalAgClosed = totalAgClosed;
            TotalAgSuspended = totalAgSuspended;
            TotalMemActive = totalMemActive;
            TotalMemClosed = totalMemClosed;
            TotalMemSuspended = totalMemSuspended;
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

        public double Credit { get; }

        public bool ClosedReason { get; }

        public bool Enabled { get; }

        public bool UplineEnabled { get; }

        public bool Closed { get; }

        public bool Suspended { get; }

        public double NetPosition { get; }

        public DateTime CreateDate { get; }

        public DateTime LastLogged { get; }

        public string LastLoginIP { get; }

        public DateTime PwdExpiry { get; }

        public double MasterCredit_Min { get; }

        public double MasterCredit_Max { get; }

        public double AgentCredit_Min { get; }

        public double AgentCredit_Max { get; }

        public double MemMaxCredit_Min { get; }

        public double MemMaxCredit_Max { get; }

        public int TotalAgActive { get; }

        public int TotalAgClosed { get; }

        public int TotalAgSuspended { get; }

        public int TotalMemActive { get; }

        public int TotalMemClosed { get; }

        public int TotalMemSuspended { get; }
    }
}
