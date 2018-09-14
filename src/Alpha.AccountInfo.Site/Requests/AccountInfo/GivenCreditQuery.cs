namespace Alpha.AccountInfo.Site.Requests.ProductStatus
{
    using AccountInfo.Models;
    using Alpha.AccountInfo.Site.ViewModels.AccountInfo;
    using MediatR;
    using System.Linq;

    public class GivenCreditQuery : AccountInfoQueryBase, IRequest<GivenCreditViewModel>
    {
        public GivenCreditQuery(IUser viewer) : base(viewer)
        {
            Language = viewer.Language;
        }

        public string SearchAccountName { get; set; }

        public byte SearchAccountRoleId { get; set; }

        public decimal CurrentGivenCredit { get; set; }

        public string Language { get; }

        public override bool IsValid() => 
            base.IsValid() &&
            IsValidSearchAccountRoleId(SearchAccountRoleId) && 
            !string.IsNullOrEmpty(SearchAccountName);
    }
}