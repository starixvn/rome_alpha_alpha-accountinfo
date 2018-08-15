namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;

    public class GivenCreditQuery : BaseRequest, IRequest<GivenCreditViewModel>
    {
        public GivenCreditQuery(IUser viewer)
            : base(viewer)
        {
            Language = viewer.Language;
        }

        public int SearchAccountId { get; set; }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public decimal CurrentGivenCredit { get; set; }

        public string Language { get; }

        public override bool IsValid() => SearchAccountId > 0;
    }
}