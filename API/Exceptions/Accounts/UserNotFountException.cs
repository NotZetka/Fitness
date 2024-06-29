namespace API.Exceptions.Accounts
{
    public class UserNotFountException : Exception
    {
        public UserNotFountException(string usernameOrEmail)
        {
            UsernameOrEmail = usernameOrEmail;
        }

        public string UsernameOrEmail { get; }
    }
}
