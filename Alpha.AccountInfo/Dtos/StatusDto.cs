namespace Alpha.AccountInfo.Dtos
{
    public class StatusDto
    {
        public int ProductId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsSync { get; set; }

        public bool IsHidden { get; set; }

        public bool IsHiddenUpline { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDisabledDownline { get; set; }

        public bool IsSyncedDownline { get; set; }

        public bool IsDisabledUpline { get; set; }

        public bool IsSyncedUpline { get; set; }

        public bool HasDownline { get; set; }
    }
}
