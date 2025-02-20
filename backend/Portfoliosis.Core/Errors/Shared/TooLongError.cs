namespace Portfoliosis.Core.Errors.Shared;

public class TooLongError : Error
{
    public TooLongError(string code, string subject, int expectedLength, int actualLength)
        : base(
            code,
            $"{subject} is too long: {actualLength} characters. Expected size: under {expectedLength} characters.",
            ErrorType.Validation
        )
    {

    }
}
