namespace Alpha.AccountInfo.Tests.Fixtures
{
    using Fanex.Data;
    using NSubstitute;

    public class ObjectDbFixture
    {
        public ObjectDbFixture()
        {
            ObjectDb = Substitute.For<IObjectDb>();
            DbSettingProviderManager
                .StartTestingSession()
                .Run();
            //AccountObjectDbFactory.CreateInstance = (settingKey) => ObjectDb;
        }

        public IObjectDb ObjectDb { get; private set; }
    }
}