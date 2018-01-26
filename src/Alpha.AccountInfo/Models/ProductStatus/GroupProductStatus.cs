namespace Alpha.AccountInfo.Models.ProductStatus
{
    using System.Collections.Generic;

    public class GroupProductStatus
    {
        public GroupProductStatus(
            int groupId,
            IEnumerable<ProductStatusModel> productStatuses)
            : this(groupId, null, productStatuses)
        {
        }

        public GroupProductStatus(
            int groupId,
            string groupName,
            IEnumerable<ProductStatusModel> productStatuses)
        {
            GroupId = groupId;
            GroupName = groupName;
            ProductStatuses = productStatuses;
        }

        public int GroupId { get; }

        public string GroupName { get; }

        public IEnumerable<ProductStatusModel> ProductStatuses { get; }
    }
}
