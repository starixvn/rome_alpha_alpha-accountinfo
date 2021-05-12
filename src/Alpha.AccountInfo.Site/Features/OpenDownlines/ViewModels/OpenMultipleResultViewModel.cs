namespace Alpha.AccountInfo.Site.Features.SearchAccount.ViewModels
{
    using Alpha.AccountInfo.Features.OpenDownlines.AppResources;
    using Alpha.AccountInfo.Features.Shared.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class OpenResult
    {
        public OpenResult(string username, bool isClosed, Result result)
        {
            Username = username;
            IsClosed = isClosed;
            Result = Result.CreateErrorResult(result.Code, $"{username}: {result.Message}");
        }

        public string Username { get; }

        public bool IsClosed { get; }

        public Result Result { get; }
    }

    public class OpenMultipleResultViewModel
    {
        private readonly IEnumerable<OpenResult> openResults;

        public OpenMultipleResultViewModel(IEnumerable<OpenResult> openResults)
        {
            this.openResults = openResults;
        }

        public IEnumerable<Result> Results
        {
            get
            {
                var failedResults = new List<Result>();
                var successfullyCloseUsernames = new List<string>();
                var successfullyOpenUsernames = new List<string>();

                openResults.ToList().ForEach(openResult =>
                {
                    if (openResult.Result.IsSuccess)
                    {
                        if (openResult.IsClosed)
                        {
                            successfullyCloseUsernames.Add(openResult.Username);
                        }
                        else
                        {
                            successfullyOpenUsernames.Add(openResult.Username);
                        }
                    }
                    else
                    {
                        failedResults.Add(openResult.Result);
                    }
                });

                var successfullyResults = new List<Result>();
                if (successfullyOpenUsernames.Count > 0)
                {
                    successfullyResults.Add(BuildSussesfullyMessage(
                        OpenDownlinesResources.OpenedSuccessfully,
                        successfullyOpenUsernames));
                }

                if (successfullyCloseUsernames.Count > 0)
                {
                    successfullyResults.Add(BuildSussesfullyMessage(
                        OpenDownlinesResources.ClosedSuccessfully,
                        successfullyCloseUsernames));
                }

                return successfullyResults.Concat(failedResults);
            }
        }

        private static Result BuildSussesfullyMessage(string formatedMessages, IEnumerable<string> usernames)
            => Result.CreateSuccessResult(string.Format(formatedMessages, string.Join(", ", usernames)));
    }
}