namespace Alpha.AccountInfo.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataAccesses;
    using Dtos;
    using Models;

    public class StatusServiceImpl : StatusService
    {
        private readonly StatusDataAccess statusDataAccess;

        public StatusServiceImpl(StatusDataAccess statusDataAccess)
        {
            this.statusDataAccess = statusDataAccess;
        }

        public IEnumerable<Status> BuildStatusList(IEnumerable<StatusDto> statusDtos)
        {
            var statusConfigItems = statusDataAccess.GetStatusConfigItems();

            return Enumerable.Empty<Status>();
        }
    }
}
