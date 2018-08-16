namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class SuspendedCommand : CustomerStatusCommand
    {
        public SuspendedCommand(IUser viewer)
            : base(viewer)
        {
            ActorName = viewer.Name;
        }

        public string ActorName { get; }

        public bool Suspended { get; set; }

        public bool SyncedCasino { get; set; }
    }
}