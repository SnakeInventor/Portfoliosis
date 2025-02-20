using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Message;

public sealed class TextTooLongError : TooLongError
{
    public TextTooLongError(int expectedLength, int actualLength) : base(typeof(TextTooLongError).FullName!, "Message text", expectedLength, actualLength)
    {
    }
}
