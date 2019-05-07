namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.Shared.Models;
    using Alpha.AccountInfo.Features.UnsuspendDownlines.AppResources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnsuspendResult
    {
        public UnsuspendResult(string username, bool suspended, Result result)
        {
            Username = username;
            Suspended = suspended;
            Result = Result.CreateErrorResult(result.Code, $"{username}: {result.Message}");
        }

        public string Username { get; private set; }

        public bool Suspended { get; private set; }

        public Result Result { get; private set; }
    }

    public class UnsuspendMultipleResultViewModel
    {
        private readonly IEnumerable<UnsuspendResult> unsuspendResults;

        public UnsuspendMultipleResultViewModel(IEnumerable<UnsuspendResult> unsuspendResults)
        {
            this.unsuspendResults = unsuspendResults;
        }

        public IEnumerable<Result> Results {
            get
            {
                var results = new List<Result>();
                var sussessfullySusependUsernames = new List<string>();
                var sussessfullyUnSusependUsernames = new List<string>();

                unsuspendResults.ToList().ForEach(unsuspendResult =>
                {
                    if (unsuspendResult.Result.IsSuccess)
                    {
                        if (unsuspendResult.Suspended)
                        {
                            sussessfullySusependUsernames.Add(unsuspendResult.Username);
                        }
                        else
                        {
                            sussessfullyUnSusependUsernames.Add(unsuspendResult.Username);
                        }
                    }
                    else
                    {
                        results.Add(unsuspendResult.Result);
                    }
                });

                if (sussessfullyUnSusependUsernames.Count > 0)
                {
                    results.Add(BuildSussesfullyMessage(UnsuspendDownlinesResources.UnsuspendedSuccessfully, sussessfullyUnSusependUsernames));
                }

                if (sussessfullySusependUsernames.Count > 0)
                {
                    results.Add(BuildSussesfullyMessage(UnsuspendDownlinesResources.SuspendedSuccessfully, sussessfullySusependUsernames));
                }

                return results;
            }
        }

        private static Result BuildSussesfullyMessage(string formatedMessages, IEnumerable<string> usernames)
            => Result.CreateSuccessResult(String.Format(formatedMessages, string.Join(", ", usernames)));
    }

}