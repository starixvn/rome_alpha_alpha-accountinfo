namespace Alpha.AccountInfo.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AccountInfo.Services;
    using DataAccesses;
    using NUnit.Framework;
    using NSubstitute;
    using AccountInfo.Services.Impl;
    using Dtos;

    [TestFixture]
    public class StatusServiceTests
    {
        private StatusService statusService;
        private StatusDataAccess statusDataAccess;

        [SetUp]
        public void Setup()
        {
            statusDataAccess = Substitute.For<StatusDataAccess>();
            statusService = new StatusServiceImpl(statusDataAccess);
        }

        [Test]
        public void BuildStatusList_ExecuteGetStatusConfigItems()
        {
            //// Act
            statusService.BuildStatusList(Enumerable.Empty<StatusDto>());

            //// Assert
            statusDataAccess.Received().GetStatusConfigItems();
        }

        //[Test]
        //public void BuildStatusList_
    }
}
