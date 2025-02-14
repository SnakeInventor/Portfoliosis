using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public sealed class EmailTooLongError : TooLongError
{
    public EmailTooLongError(int expectedLength, int actualLength) : base(typeof(EmailTooLongError).FullName!, "Email", expectedLength, actualLength)
    {
    }
}
