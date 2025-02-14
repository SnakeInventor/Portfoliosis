using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Message;

public class TextEmptyError : EmptyError
{
    public TextEmptyError() : base(typeof(TextEmptyError).FullName!, "Text provided in the message")
    {
    }
}
