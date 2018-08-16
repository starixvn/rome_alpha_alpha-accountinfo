namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using MediatR;

    public class OutrightCommand : BaseRequest, IRequest<Result>
    {
        private readonly IUser viewer;

        public OutrightCommand(IUser viewer)
            : base(viewer)
        {
            this.viewer = viewer;
        }

        public int SearchAccountId { get; set; }

        public bool DisableOutright { get; set; }

        public int ActorId => viewer.Id;

        public int SubAccountId => viewer.SubAccId;

        public string Language => viewer.LangKey;

        public string IP => viewer.IP;

        public override bool IsValid() => SearchAccountId > 0;
    }
}