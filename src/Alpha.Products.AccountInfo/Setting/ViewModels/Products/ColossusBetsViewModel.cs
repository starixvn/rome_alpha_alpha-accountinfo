namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;
    using static Alpha.Products.Core.Shared.ProductConstants;

    public class ColossusBetsViewModel : ProductSettingItemViewModel
    {
        public ColossusBetsViewModel(ISearchAccountInfoRequest request) : base(request, ColossusBets)
        {
        }

        protected override string BaseAddress
            => $"{OldMainUrl}customersetting/ColossusBetSetting/ColossusBetSetting/EditSetting";

        protected override Dictionary<string, string> QueryStringParamaters
        {
            get
            {
                var paramaters = new Dictionary<string, string>
                {
                    ["custid"] = SearchAccount.Id.ToString(),
                    ["username"] = SearchAccount.Username,
                    ["roleid"] = SearchAccount.CusomerLevelValue.ToString(),
                    ["userid"] = Actor.Id.ToString(),
                    ["subAccId"] = Actor.SubAccountId.ToString(),
                    ["isMultipleEdit"] = "0"
                };

                var searchAccountUplineIdQueryStringName =
                    Request.Actor.IsSuper
                    ? "srecommend"
                    : (Request.Actor.IsMaster ? "mrecommend" : "recommend");

                paramaters[searchAccountUplineIdQueryStringName] = SearchAccount.Upline.Id.ToString();

                return paramaters;
            }
        }

        public override bool Visible
            => !Actor.ProductStatusOf(ProductId).Hidden
               && Request.ActorIsEdittingDirectDownline;
    }
}