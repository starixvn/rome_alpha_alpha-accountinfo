namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class DoubleCommCommand : CustomerStatusCommand
    {
        public DoubleCommCommand(IUser viewer)
            : base(viewer)
        {
        }

        public bool DoubleComm { get; set; }
    }
}