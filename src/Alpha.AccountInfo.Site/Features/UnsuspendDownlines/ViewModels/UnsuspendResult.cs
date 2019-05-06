namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;

    public class UnsuspendResult
    {
        public UnsuspendResult(string username, bool suspended , Result result)
        {
            Username = username;
            Suspended = suspended;
            Result = result;
        }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public Result Result { get; private set; }
    }
}