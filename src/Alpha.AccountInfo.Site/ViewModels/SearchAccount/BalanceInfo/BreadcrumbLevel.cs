namespace Alpha.AccountInfo.Site.ViewModels.SearchAccount
{
    public class BreadcrumbLevel
    {
        public BreadcrumbLevel(
            int custId,
            string custName,
            byte custLevelId)
        {
            CustId = custId;
            CustName = custName;
            CustLevelId = custLevelId;
        }
        
        public int CustId { get; }

        public string CustName { get; }

        public byte CustLevelId { get; }
    }
}