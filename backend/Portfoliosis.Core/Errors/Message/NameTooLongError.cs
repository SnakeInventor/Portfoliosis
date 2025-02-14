using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Message;

public sealed class NameTooLongError : TooLongError
{
    public NameTooLongError(int expectedLength, int actualLength) : base(typeof(NameTooLongError).FullName!, "Name provided in the message", expectedLength, actualLength)
    {

    }
}
