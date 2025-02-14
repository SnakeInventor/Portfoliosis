using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public sealed class LocalTooLongError : TooLongError
{
    public LocalTooLongError(int expectedLength, int actualLength) : base(typeof(LocalTooLongError).FullName!, "Local (part before @)", expectedLength, actualLength)
    {
    }
}