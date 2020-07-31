namespace Alpha.AccountInfo.Features.Shared.Params
{
    public class CheckUMParam
    {
        public CheckUMParam(
            int custId,
            int productId,
            int sportType,
            byte settingType,
            byte functionType,
            string language)
        {
            CustId = custId;
            ProductId = productId;
            SportType = sportType;
            SettingType = settingType;
            FunctionType = functionType;
            Language = language;
        }

        public int CustId { get; }

        public int ProductId { get; }

        public int SportType { get; }

        public byte SettingType { get; }

        public byte FunctionType { get; }

        public string Language { get; }
    }
}
