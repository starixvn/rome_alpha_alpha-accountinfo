using System.Collections.Generic;
using Alpha.AccountInfo.Features.SearchAccount.Models;
using Alpha.AccountInfo.Features.Shared.Models;
using MediatR;

namespace Alpha.AccountInfo.Site.Features.SearchAccount.Requests
{
    public class TotalDownlineQuery : SearchAccountQueryBase, IRequest<IEnumerable<TotalDownlinesSummary>>
    {
        public TotalDownlineQuery(IUser viewer) : base(viewer)
        {
        }

        public byte RoleId { get; set; }
    }
}