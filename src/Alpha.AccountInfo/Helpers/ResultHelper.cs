namespace Alpha.AccountInfo.Helpers
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public static class ResultHelper
    {
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
