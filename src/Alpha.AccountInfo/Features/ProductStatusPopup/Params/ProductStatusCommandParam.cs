namespace Alpha.AccountInfo.Params.ProductStatus
{
    using System.Collections.Generic;
    using Alpha.AccountInfo.Features.Shared.Models;

    public class ProductStatusCommandParam
    {
#pragma warning disable S107 // Methods should not have too many parameters
        public ProductStatusCommandParam(
            int productId,
            int custId,
            UserLevel custRole,
            int uplineId,
            UserLevel uplineLevel,
            int actorId,
            string actorName,
            int subAccId,
            string langKey,
            string ip,
            IEnumerable<BetTypeStatusParam> productStatusParams,
            string language)
#pragma warning restore S107 // Methods should not have too many parameters
        {
            ProductId = productId;
            CustId = custId;
            CustRole = custRole;
            UplineId = uplineId;
            UplineLevel = uplineLevel;
            ActorId = actorId;
            ActorName = actorName;
            SubAccId = subAccId;
            LangKey = langKey;
            IP = ip;
            ProductStatusParams = productStatusParams;
            Language = language;
        }

        public int ProductId { get; }

        public int CustId { get; }

        public UserLevel CustRole { get; }

        public int UplineId { get; }

        public UserLevel UplineLevel { get; }

        public int ActorId { get; }

        public string ActorName { get; }

        public int SubAccId { get; }

        public string LangKey { get; }

        public string IP { get; }

        public IEnumerable<BetTypeStatusParam> ProductStatusParams { get; }

        public string Language { get; }
    }
}