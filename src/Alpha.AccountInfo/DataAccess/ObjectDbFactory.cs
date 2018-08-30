namespace Alpha.AccountInfo.DataAccess
{
    using Fanex.Data;
    using System;

    public static class AccountObjectDbFactory
    {
        [ThreadStatic]
        private static Func<string, IObjectDb> createInstance;

        public static Func<string, IObjectDb> CreateInstance
        {
            get { return createInstance ?? ((settingKey) => ObjectDbFactory.CreateInstance(settingKey)); }
            internal set { createInstance = value; }
        }
    }
}
