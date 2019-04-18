namespace Alpha.AccountInfo.Features.UnsuspendDownlines
{
    using Alpha.AccountInfo.Features.UnsuspendDownlines.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface UnsuspendDownlinesService
    {
        Task<UnsuspendModel> GetUnsuspendModel();

        Task<UnsuspendDownlinesModel> GetUnsuspendDownlinesModel();
    }

    public class UnsuspendDownlinesServiceImpl : UnsuspendDownlinesService
    {
        public async Task<UnsuspendModel> GetUnsuspendModel()
        {
           var unsuspendByLevel = new List<UnsuspendByLevelModel>();

            for (int i = 3; i > 0; i--)
            {
                unsuspendByLevel.Add(new UnsuspendByLevelModel((byte)i, i * 15, i *30));
            }

            return new UnsuspendModel(unsuspendByLevel);
        }

        public async Task<UnsuspendDownlinesModel> GetUnsuspendDownlinesModel()
        {
            var unsuspendDownlines = new List<UnsuspendDownlineDto>();

           for(int i = 0; i < 99; i++)
            {
                unsuspendDownlines.Add( new UnsuspendDownlineDto($"Username-{i}", i, i < 30 ? (byte)1 : i < 60 ? (byte)2 : (byte)3));
            }

            return new UnsuspendDownlinesModel(unsuspendDownlines.GroupBy(item => item.RoleId).Select(group => {
                return new UnsuspendDownlineByLevelModel(group.Key, group);
            }), 5);
        }
    }
}
