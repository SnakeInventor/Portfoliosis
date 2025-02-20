namespace Portfoliosis.Core.Errors.Email;

public class EmailInvalidFormatError : Error
{
    public EmailInvalidFormatError(string email) : base(typeof(EmailInvalidFormatError).FullName!, $"Email format is invalid! Provided email: {email}", ErrorType.Validation)
    {
    }
}
