namespace Alpha.AccountInfo.Dtos
{
    public class StatusConfigItem
    {
        public int ProductId { get; set; }

        public bool UseCore { get; set; }

        public string StatusSettingKey { get; set; }

        public string Control { get; set; }

        public string UpdateActionUrl { get; set; }
    }
}