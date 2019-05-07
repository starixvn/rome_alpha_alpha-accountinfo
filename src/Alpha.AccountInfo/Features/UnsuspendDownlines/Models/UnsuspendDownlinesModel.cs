namespace Alpha.AccountInfo.Features.UnsuspendDownlines.Models
{
    using System.Collections.Generic;

    public class UnsuspendDownlineDto
    {
        public UnsuspendDownlineDto() { }

        public UnsuspendDownlineDto(string username, byte roleId) {
            Username = username;
            RoleId = roleId;
        }

        public string Username { get; private set; }

        public byte RoleId { get; private set; }
    }

    public class UnsuspendDownlineByLevelModel
    {
        public UnsuspendDownlineByLevelModel(byte roleId, IEnumerable<UnsuspendDownlineDto> unsuspendDownlines)
        {
            UnsuspendDownlines = unsuspendDownlines;
            RoleId = roleId;
        }

        public IEnumerable<UnsuspendDownlineDto> UnsuspendDownlines { get; private set; }

        public byte RoleId { get; private set; }
    }

    public class UnsuspendViewListModel
    {
        public UnsuspendViewListModel(
            IEnumerable<UnsuspendDownlineByLevelModel> levels, 
            int totalRows)
        {
            Levels = levels;
            TotalRows = totalRows;
        }

        public IEnumerable<UnsuspendDownlineByLevelModel> Levels { get; private set; }

        public int TotalRows { get; private set; }
    }
}
