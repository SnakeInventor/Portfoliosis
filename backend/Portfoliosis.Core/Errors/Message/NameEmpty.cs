using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Message;

public class NameEmptyError : EmptyError
{
    public NameEmptyError() : base(typeof(NameEmptyError).FullName!, "Name provided in the message")
    {
    }
}
