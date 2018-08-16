namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;

    public class GivenCreditCommand : CustomerStatusCommand
    {
        public GivenCreditCommand(IUser viewer)
            : base(viewer)
        {
            ActorRoleId = viewer.LevelId;
        }

        public byte SearchAccountRoleId { get; set; }

        public decimal CreditAmount { get; set; }

        public byte ActorRoleId { get; }
    }
}