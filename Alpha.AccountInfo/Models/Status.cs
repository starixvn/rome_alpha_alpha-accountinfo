namespace Alpha.AccountInfo.Models
{
    public class Status
    {
        public int ProductId { get; set; }

        public int SportTypeId { get; set; }

        public int BetTypeId { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsSynced { get; set; }

        public bool IsHidden { get; set; }

        public Status Upline { get; set; }

        public Status Downline { get; set; }

        public bool HasDownline { get; set; }

        public VisibilityType VisibilityType { get; set; }

        public DisplayType DisplayType { get; set; }

        public string UpdateActionUrl { get; set; }
    }
}