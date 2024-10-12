namespace API.Handlers.Accounts.Register
{
    public class RegisterCommand
    {
        public string Role { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BankAccountNumber { get; set; }
    }
}
