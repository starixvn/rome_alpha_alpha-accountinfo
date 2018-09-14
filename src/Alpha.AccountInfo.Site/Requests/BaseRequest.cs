namespace Alpha.AccountInfo.Site.Requests
{
    using AccountInfo.Models;

    public abstract class BaseRequest
    {
        protected BaseRequest(IUser viewer)
        {
        }

        public abstract bool IsValid();

        internal bool IsValidName(string name) => !string.IsNullOrEmpty(name);
    }
}