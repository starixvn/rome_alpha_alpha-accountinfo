namespace Alpha.AccountInfo.Models.ProductStatus
{
    using System.Collections.Generic;

    public class GroupProductStatus
    {
        public GroupProductStatus(
            int groupId,
            IEnumerable<ProductStatusModel> productStatuses)
            : this(groupId, null, null, productStatuses)
        {
        }

        public GroupProductStatus(
            int groupId,
            string productName,
            IEnumerable<ProductStatusModel> productStatuses)
            : this(groupId, null, productName, productStatuses)
        {
        }

        public GroupProductStatus(
            int groupId,
            string groupName,
            string productName,
            IEnumerable<ProductStatusModel> productStatuses)
        {
            GroupId = groupId;
            GroupName = groupName;
            ProductName = productName;
            ProductStatuses = productStatuses;
        }

        public int GroupId { get; }

        public string GroupName { get; }

        public string ProductName { get; }

        public IEnumerable<ProductStatusModel> ProductStatuses { get; }
    }
}
