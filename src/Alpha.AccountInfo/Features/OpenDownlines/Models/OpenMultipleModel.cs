namespace Alpha.AccountInfo.Features.OpenDownlines.Models
{
    using System.Collections.Generic;

    public class OpenMultipleModel
    {
        public OpenMultipleModel(
            int searchAccountId,
            string username,
            bool isClosed,
            IEnumerable<OpenDownlineModel> downlines)
        {
            SearchAccountId = searchAccountId;
            Username = username;
            IsClosed = isClosed;
            Downlines = downlines;
        }

        public int SearchAccountId { get; }

        public string Username { get; }

        public bool IsClosed { get; }

        public IEnumerable<OpenDownlineModel> Downlines { get; }
    }

    public class OpenSearchAccountModel
    {
        public OpenSearchAccountModel()
        {
        }

        public OpenSearchAccountModel(
            int searchAccountId,
            string username,
            bool isClosed)
        {
            SearchAccountId = searchAccountId;
            Username = username;
            IsClosed = isClosed;
        }

        public int SearchAccountId { get; }

        public string Username { get; }

        public bool IsClosed { get; }
    }

    public class OpenDownlineModel
    {
        public OpenDownlineModel()
        {
        }

        public OpenDownlineModel(
            int searchAccountId,
            byte downlineRoleId,
            int affectedDownlines,
            int unchangedDownlines)
        {
            SearchAccountId = searchAccountId;
            DownlineRoleId = downlineRoleId;
            AffectedDownlines = affectedDownlines;
            UnchangedDownlines = unchangedDownlines;
        }

        public int SearchAccountId { get; }

        public byte DownlineRoleId { get; }

        public int AffectedDownlines { get; }

        public int UnchangedDownlines { get; }
    }
}