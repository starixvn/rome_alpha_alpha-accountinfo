namespace Alpha.Products.AccountInfo.Setting.ViewModels.Products
{
    using System.Collections.Generic;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    /// <summary>
    ///     View model for all products use DCS
    /// </summary>
    public class DynamicProductViewModel : ProductSettingItemViewModel
    {
        public DynamicProductViewModel(ISearchAccountInfoRequest request, int productId)
            : base(request, productId)
        {
        }

        public override string Text => Request.GetProductName(ProductId);

        protected override string BaseAddress 
            => $"{OldMainUrl}dcs/viewcustomersetting";

        protected override Dictionary<string, string> QueryStringParamaters
        {
            get
            {
                var query = new Dictionary<string, string>
                {
                    ["customerLevel"] = SearchAccount.CustomerLevel.Value.ToString(),
                    ["customerId"] = SearchAccount.Id.ToString(),
                    ["productId"] = ProductId.ToString()
                };

                if (SearchAccount.IsMember)
                {
                    query.Add("uplinelevel", Actor.CustomerLevel.Value.ToString());
                }

                return query;
            }
        }

        protected override Dictionary<string, string> QueryStringParamatersUpdateMultiple
          => new Dictionary<string, string>
          {
              ["isGetMultipleDCS"] = "1",
              ["isMultiple"] = "true",
              ["productId"] = ProductId.ToString(),
              ["uplineId"] = Actor.Id.ToString(),
              ["actorId"] = Actor.Id.ToString(),
              //["customerIds"] = Request.GetData<string>("customerIds"),
              //["usernames"] = Request.GetData<string>("usernames"),
              //["customerNames"] = Request.GetData<string>("usernames"),
              //usernames, customerNames // Bind at client
              //customerIds
              ["customerLevel"] = SearchAccount.CusomerLevelValue.ToString(),
              ["subAccId"] = Actor.SubAccountId.ToString(),
              ["formType"] = "1",
              ["isForcingMode"] = "false"
          };

        public override bool IsUpdateMultiple => true;
        
        protected override string BaseAddressUpdateMultiple => BaseAddress;

        public override bool Visible => base.Visible && Actor.CanSeeProduct(ProductId);
    }
}