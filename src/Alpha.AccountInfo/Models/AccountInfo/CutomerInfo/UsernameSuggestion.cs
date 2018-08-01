namespace Alpha.AccountInfo.Models.AccountInfo
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

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}
