namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class OutrightCommand : CustomerStatusCommand
    {
        public OutrightCommand(IUser viewer)
            : base(viewer)
        {
        }

        public bool DisableOutright { get; set; }
    }
}