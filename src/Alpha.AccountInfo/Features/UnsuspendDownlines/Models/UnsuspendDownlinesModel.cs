namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System.Collections.Generic;

    public class UnsuspendDownlineDto
    {
        public UnsuspendDownlineDto() { }

        public UnsuspendDownlineDto(string username, int custId, byte roleId) {
            Username = username;
            CustId = custId;
            RoleId = roleId;
        }

        public string Username { get; private set; }

        public int CustId { get; private set; }

        public byte RoleId { get; private set; }
    }

    public class UnsuspendDownlineByLevelModel
    {
        public UnsuspendDownlineByLevelModel() { }

        public UnsuspendDownlineByLevelModel(byte roleId, IEnumerable<UnsuspendDownlineDto> unsuspendDownlines)
        {
            UnsuspendDownlines = unsuspendDownlines;
            RoleId = roleId;
        }

        public IEnumerable<UnsuspendDownlineDto> UnsuspendDownlines { get; private set; }

        public byte RoleId { get; private set; }
    }

    public class UnsuspendDownlinesModel
    {
        public UnsuspendDownlinesModel() { }

        public UnsuspendDownlinesModel(
            IEnumerable<UnsuspendDownlineByLevelModel> unsuspendDownlinesByLevel, 
            int totalPages)
        {
            UnsuspendDownlinesByLevel = unsuspendDownlinesByLevel;
            TotalPages = totalPages;
        }

        public IEnumerable<UnsuspendDownlineByLevelModel> UnsuspendDownlinesByLevel { get; private set; }

        public int TotalPages { get; private set; }
    }
}
