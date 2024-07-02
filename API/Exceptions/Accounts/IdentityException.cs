using Microsoft.AspNetCore.Identity;

namespace API.Exceptions.Accounts
{
    public class IdentityException : Exception
    {
        public IdentityException(IEnumerable<IdentityError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<IdentityError> Errors { get; }
    }
}
