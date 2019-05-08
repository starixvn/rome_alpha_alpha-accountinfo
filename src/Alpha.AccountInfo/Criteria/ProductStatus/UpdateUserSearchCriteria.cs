namespace Alpha.AccountInfo.Criteria.ProductStatus
{
    using Fanex.Data.Repository;

    public class UpdateUserSearchCriteria : CriteriaBase
    {
        public UpdateUserSearchCriteria(int userId, string search, string firstName, string lastName, string nickname)
        {
            UserId = userId;
            Search = search;
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
        }

        public int UserId { get; }

        public string Search { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Nickname { get; }

        public string System { get; } = "Alpha";

        public override string GetSettingKey() => "Acc_CustomerUpdUserSearch";

        public override bool IsValid() => UserId > 0 && !string.IsNullOrWhiteSpace(Search);
    }
}