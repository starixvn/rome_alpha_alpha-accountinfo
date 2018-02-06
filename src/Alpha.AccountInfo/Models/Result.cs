namespace Alpha.AccountInfo.Models
{
    using System.Net;
    using AppResources;

    public class Result
    {
        private Result(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }

        public string Message { get; }

        public static Result CreateSuccessResult(string message = null)
            => new Result(0, message);

        public static Result CreateErrorResult(int code, string message)
            => new Result(code, message);

        public static Result CreateForbiddenResult()
            => new Result((int)HttpStatusCode.Forbidden, Resources.NotAuthorized);

        public bool IsSuccess() => Code == 0;
    }
}
