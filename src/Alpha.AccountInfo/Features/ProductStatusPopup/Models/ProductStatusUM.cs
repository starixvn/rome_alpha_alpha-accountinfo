namespace Alpha.AccountInfo.Features.ProductStatusPopup.Models
{
    using System;

    public class ProductStatusUM
    {
        public ProductStatusUM()
        {
        }

        public ProductStatusUM(
            int productId,
            int sportType,
            int betType,
            DateTime beginTime,
            DateTime endTime)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            BeginTime = beginTime;
            EndTime = endTime;
        }

        public int ProductId { get; }

        public int SportType { get; }

        public int BetType { get; }

        public DateTime BeginTime { get; }

        public DateTime EndTime { get; }
    }
}
