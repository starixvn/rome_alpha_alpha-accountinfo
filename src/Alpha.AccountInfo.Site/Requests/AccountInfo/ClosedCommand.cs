namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class ClosedCommand : CustomerStatusCommand
    {
        public ClosedCommand(IUser viewer)
            : base(viewer)
        {
            ActorName = viewer.Name;
        }

        public string ActorName { get; }

        public bool Closed { get; set; }

        public bool SyncedCasino { get; set; }
    }
}