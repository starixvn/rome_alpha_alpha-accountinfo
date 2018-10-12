namespace Alpha.AccountInfo.Tests.Fixtures
{
    using Alpha.AccountInfo.DataAccess;
    using Fanex.Data;
    using NSubstitute;

    public class ObjectDbFixture
    {
        public ObjectDbFixture()
        {
            ObjectDb = Substitute.For<IObjectDb>();
            //AccountObjectDbFactory.CreateInstance = (settingKey) => ObjectDb;
        }

        public IObjectDb ObjectDb { get; private set; }
    }
}
