namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class CustomerStatusModel
    {
        public CustomerStatusModel(
            bool hasDownline,
            bool disableAutoMPT,
            bool closed,
            bool uplineClosed,
            bool suspended,
            bool uplineSuspend,
            bool transferOption,
            bool doubleCommLosing,
            bool uplineDoubleCommLosing,
            bool disableOutright,
            bool uplineDisableOutright)
        {
            HasDownline = hasDownline;
            DisableAutoMPT = disableAutoMPT;

            Closed = closed;
            UplineClosed = uplineClosed;

            Suspended = suspended;
            UplineSuspend = uplineSuspend;

            TransferOption = transferOption;

            DoubleCommLosing = doubleCommLosing;
            UplineDoubleCommLosing = uplineDoubleCommLosing;

            DisableOutright = disableOutright;
            UplineDisableOutright = uplineDisableOutright;
        }

        public bool HasDownline { get; }

        public bool Closed { get; }

        public bool UplineClosed { get; }

        public bool Suspended { get; }

        public bool UplineSuspend { get; }

        public bool TransferOption { get; }

        public bool DoubleCommLosing { get; }

        public bool UplineDoubleCommLosing { get; }

        public bool DisableAutoMPT { get; }

        public bool DisableOutright { get; }

        public bool UplineDisableOutright { get; }
    }
}
