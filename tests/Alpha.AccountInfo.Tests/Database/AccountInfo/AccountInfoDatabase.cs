namespace Alpha.AccountInfo.Tests.Database
{
    using Helpers;
    using System.Data;

    public class AccountInfoDatabase
    {
        public static DataSet OnlineListData => FileHelper.LoadDataSet("\\Database\\AccountInfo\\OnlineListHasData.xml");

        public static DataSet DownlineListData => FileHelper.LoadDataSet("\\Database\\AccountInfo\\DownlineListHasData.xml");

        public static DataSet CustomerAllStatusData => FileHelper.LoadDataSet("\\Database\\AccountInfo\\CustomerAllStatusHasData.xml");
    }
}
