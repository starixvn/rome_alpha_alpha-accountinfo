namespace Alpha.AccountInfo.Features.SearchAccount.Models.AccountInfo
{
    public class UsernameSuggestion
    {
        public UsernameSuggestion()
        {
        }

        public UsernameSuggestion(string userName, string firstName, string lastName)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public string UserName { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
    }
}
