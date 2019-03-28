namespace Alpha.AccountInfo.Site.Features.Shared
{
    using Alpha.AccountInfo.Features.Shared.Models;

    public abstract class BaseRequest
    {
        protected BaseRequest(IUser viewer)
        {
        }

        public abstract bool IsValid();

        internal bool IsValidName(string name) => !string.IsNullOrEmpty(name);
    }
}