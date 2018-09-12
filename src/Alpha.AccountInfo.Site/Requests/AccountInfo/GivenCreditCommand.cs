namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using MediatR;

    public class GivenCreditCommand : BaseRequest, IRequest<Result>
    {
        private readonly IUser viewer;

        public GivenCreditCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public int SearchAccountId { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public decimal CreditAmount { get; set; }

        public byte ActorRoleId => viewer.LevelId;

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid()
           => SearchAccountId > 0 
            && SearchAccountRoleId > 0
            && SearchAccountRoleId < 4;
    }
}