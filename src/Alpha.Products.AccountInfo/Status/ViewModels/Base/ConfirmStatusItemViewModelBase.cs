namespace Alpha.Products.AccountInfo.Status.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Products.AccountInfo.Requests;
    using Base;

    public abstract class ConfirmStatusItemViewModelBase : StatusItemViewModel
    {
        protected ConfirmStatusItemViewModelBase(ISearchAccountInfoRequest request, int displayOrder)
            : base(request, displayOrder)
        {
        }

        public override string Url => $"{AccountInfoUrl}SearchAccount/UpdateCustomerStatus";

        public virtual string ConfirmMessage => Request.GetResource($"{GetType().Name}Message");

        public string JsonParams =>
            string.Format("{0}{1}{2}", "{", string.Join(",", Params.Select(x => $"\"{x.Key}\":\"{x.Value.ToString()}\"")), "}");

        protected virtual string StatusName { get; }

        protected virtual bool StatusValue { get; }

        protected virtual bool SyncedCasino3rd { get; }

        private IDictionary<string, object> Params =>
            new Dictionary<string, object>
                    {
                        {"SearchAccountId", SearchAccount.Id.ToString()},
                        {"SyncedCasino3rd", SyncedCasino3rd},
                        {"StatusName", StatusName},
                        {"StatusValue", StatusValue}
                    };
    }
}