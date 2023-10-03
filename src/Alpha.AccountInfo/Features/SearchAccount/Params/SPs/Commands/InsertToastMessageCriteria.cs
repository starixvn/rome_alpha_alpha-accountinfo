namespace Alpha.AccountInfo.Features.SearchAccount.Params.SPs.Commands
{
    using System.Globalization;
    using Alpha.AccountInfo.Features.SearchAccount.Models;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Newtonsoft.Json;

    public sealed class InsertToastMessageCriteria : NonQueryCommand
    {
        private const int WarningTypeId = 3;
        private const int SiteFunctionId_ADBTChange = 2;

        public InsertToastMessageCriteria(int targetId, int creator)
        {
            TypeId = WarningTypeId;
            SiteFunctionId = SiteFunctionId_ADBTChange;
            TargetId = targetId;
            Creator = creator;

            ContentEN = GetContent("en-US");
            ContentCH = GetContent("zh-CN");
            ContentCS = GetContent("zh-TW");
            ContentTH = GetContent("th-TH");
        }

        public int TypeId { get; set; }

        public int SiteFunctionId { get; set; }

        public int TargetId { get; set; }

        public int Creator { get; set; }

        [SpParam(SqlParamName = "content_en")]
        public string ContentEN { get; set; }

        [SpParam(SqlParamName = "content_ch")]
        public string ContentCH { get; set; }

        [SpParam(SqlParamName = "content_cs")]
        public string ContentCS { get; set; }

        [SpParam(SqlParamName = "content_jp")]
        public string ContentJP { get; set; }

        [SpParam(SqlParamName = "content_it")]
        public string ContentIT => string.Empty;

        [SpParam(SqlParamName = "content_th")]
        public string ContentTH { get; set; }

        [SpParam(SqlParamName = "content_ko")]
        public string ContentKO { get; set; }

        [SpParam(SqlParamName = "content_es")]
        public string ContentES => string.Empty;

        [SpParam(SqlParamName = "content_vn")]
        public string ContentVN { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public int ToastId { get; set; }

        public override string GetSettingKey() => "Age_ToastMessage_Ins";

        public override bool IsValid() => true;

        private string GetContent(string langue)
        {
            var culture = CultureInfo.GetCultureInfo(langue);

            var content = new ToastContentModel
            {
                Title = Shared.AppResources.Resources.ResourceManager.GetString("ADBTChangeToastTitle", culture),
                Body = Shared.AppResources.Resources.ResourceManager.GetString("ADBTChangeToastMessage", culture)
            };

            return JsonConvert.SerializeObject(content, Formatting.None);
        }
    }
}