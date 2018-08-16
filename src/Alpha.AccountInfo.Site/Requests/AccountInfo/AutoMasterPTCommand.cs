namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class AutoMasterPTCommand : CustomerStatusCommand
    {
        public AutoMasterPTCommand(IUser viewer)
            : base(viewer)
        {
        }

        public bool DisableAutoMasterPT { get; set; }
    }
}