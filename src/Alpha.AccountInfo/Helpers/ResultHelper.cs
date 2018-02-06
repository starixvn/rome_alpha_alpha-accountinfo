namespace Alpha.AccountInfo.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Models;

    public static class ResultHelper
    {
        public static Result ParseKenoErrorResults(IEnumerable<Result> errors)
        {
            IList<Result> flattenErrors = new List<Result>();
            var groupErrorCode = errors.ToLookup(err => err.Code);

            foreach (var group in groupErrorCode)
            {
                flattenErrors.Add(Result.CreateErrorResult(group.Key, group.First().Message));
            }

            return Result.CreateErrorResult(
                flattenErrors[0].Code,
                string.Join("<br/>", flattenErrors.Select(err => err.Message)));
        }

        public static string ParseDCSProductMessage(string errMsg)
        {
            if (IsValidXml(errMsg))
            {
                var xmlRoot = XElement.Parse(errMsg);
                var attributes = xmlRoot.Attributes("Msg");

                return string.Join("<br/>", attributes.Select(a => a.Value));
            }

            return errMsg;
        }

        private static bool IsValidXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml) && xml.TrimStart().StartsWith("<"))
            {
                try
                {
                    XDocument.Parse(xml);
                    return true;
                }
                catch (Exception)
                {
                }
            }

            return false;
        }
    }
}
