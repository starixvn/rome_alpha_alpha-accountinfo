namespace Alpha.AccountInfo.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        byte LevelId { get; }

        int SubAccId { get; }

        string SubAccName { get; }

        int UserId { get; }

        string IP { get; }

        string LangKey { get; }

        bool IsInternal { get; }

        bool CanUpdateAccount { get; }
    }
}