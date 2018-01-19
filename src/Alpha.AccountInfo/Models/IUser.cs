namespace Alpha.AccountInfo.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        string IP { get; }

        bool IsInternal { get; }

        bool CanUpdateAccount { get; }
    }
}