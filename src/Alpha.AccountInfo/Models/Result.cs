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
        {
            return new Result(0, message);
        }

        public static Result CreateErrorResult(int code, string message)
        {
            return new Result(code, message);
        }

        public static Result CreateForbiddenResult()
        {
            return new Result((int)HttpStatusCode.Forbidden, Resources.NotAuthorized);
        }

        public bool IsSuccess()
        {
            return Code == 0;
        }
    }
}
