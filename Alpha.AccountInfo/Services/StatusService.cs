namespace Alpha.AccountInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dtos;
    using Models;

    public interface StatusService
    {
        IEnumerable<Status> BuildStatusList(IEnumerable<StatusDto> statusDtos);
    }
}
