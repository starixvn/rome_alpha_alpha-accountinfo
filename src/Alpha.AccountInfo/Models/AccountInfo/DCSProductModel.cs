namespace Alpha.AccountInfo.Models.AccountInfo
{
    public class DCSProductModel
    {
         public DCSProductModel(
             int productId,
             int sportType,
             int betType,
             bool isSync,
             bool isHidden,
             bool isDisabled,
             bool isSyncedUpline,
             bool isHiddenUpline,
             bool isDisabledUpline,
             bool hasDownline)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsSync = isSync;
            IsHidden = isHidden;
            IsDisabled = isDisabled;
            IsSyncedUpline = isSyncedUpline;
            IsHiddenUpline = isHiddenUpline;
            IsDisabledUpline = isDisabledUpline;
            HasDownline = hasDownline;
        }

       public int ProductId { get; }

       public int SportType { get; }

        public int BetType { get; }

        public bool IsSync { get; }

        public bool IsHidden { get; }

        public bool IsDisabled { get; }

        public bool IsSyncedUpline { get; }

        public bool IsHiddenUpline { get; }

        public bool IsDisabledUpline { get; }

        public bool HasDownline { get; }
    }
}
