namespace Alpha.AccountInfo.Features.SearchAccount.Models
{
    using System.Collections.Generic;

    public class DownlineDto
    {
        public DownlineDto() { }

        public DownlineDto(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }

    public class DownlineListModel
    {
        public DownlineListModel() { }

        public DownlineListModel(
         IEnumerable<DownlineDto> downlines,
         int totalAgActive,
         int totalAgClosed,
         int totalAgSuspended,
         int totalMemActive,
         int totalMemClosed,
         int totalMemSuspended,
         int totalPages)
        {
            Downlines = downlines;

            TotalAgActive = totalAgActive;
            TotalAgClosed = totalAgClosed;
            TotalAgSuspended = totalAgSuspended;

            TotalMemActive = totalMemActive;
            TotalMemClosed = totalMemClosed;
            TotalMemSuspended = totalMemSuspended;

            TotalPages = totalPages;
        }

        public DownlineListModel(
         IEnumerable<DownlineDto> downlines,
         DownlineListModel model)
            : this(
                  downlines, 
                  model.TotalAgActive, 
                  model.TotalAgClosed, 
                  model.TotalAgSuspended, 
                  model.TotalMemActive, 
                  model.TotalMemClosed, 
                  model.TotalMemSuspended,
                  model.TotalPages)
        { }

        public IEnumerable<DownlineDto> Downlines { get; private set; }

        public int TotalAgActive { get; private set; }

        public int TotalAgClosed { get; private set; }

        public int TotalAgSuspended { get; private set; }

        public int TotalMemActive { get; private set; }

        public int TotalMemClosed { get; private set; }

        public int TotalMemSuspended { get; private set; }

        public int TotalPages { get; private set; }
    }
}
