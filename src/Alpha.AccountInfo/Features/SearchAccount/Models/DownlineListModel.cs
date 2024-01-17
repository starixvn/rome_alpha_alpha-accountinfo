namespace Alpha.AccountInfo.Features.SearchAccount.Models
{
    using System.Collections.Generic;
    using Alpha.Products.Core.Shared;
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.Shared.AppResources;

    public class DownlineDto
    {
        public DownlineDto() { }

        public DownlineDto(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }

    public class DownlineListModel
    {
        public DownlineListModel() { }

        public DownlineListModel(IEnumerable<DownlineDto> downlines, int totalPages)
        {
            Downlines = downlines;
            TotalPages = totalPages;
        }

        public DownlineListModel(IEnumerable<DownlineDto> downlines, DownlineListModel model)
            : this(downlines, model.TotalPages)
        { }

        public IEnumerable<DownlineDto> Downlines { get; set; }

        public int TotalPages { get; set; }
    }

    public class TotalDownlinesSummary
    {
        private readonly Dictionary<UserLevel, string> LevelNames
         = new Dictionary<UserLevel, string>
         {
                { UserLevel.Agent, Resources.Agents},
                { UserLevel.Member,  Resources.Members}
         };

        public TotalDownlinesSummary() { }

        public TotalDownlinesSummary(int totalActive, int totalClosed, int totalSuspended, byte roleId)
        {
            TotalActive = totalActive;
            TotalClosed = totalClosed;
            TotalSuspended = totalSuspended;
            RoleId = roleId;
        }

        public int TotalActive { get; set; }

        public int TotalClosed { get; set; }

        public int TotalSuspended { get; set; }

        public byte RoleId { get; set; }

        public string LevelName => LevelNames[Enumeration.FromValue<UserLevel>(RoleId)];
    }
}
