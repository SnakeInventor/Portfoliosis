using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public class EmailEmptyError : EmptyError
{
    public EmailEmptyError() : base(typeof(EmailEmptyError).FullName!, "Email")
    {
    }
}