using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public class LocalEmptyError : EmptyError
{
    public LocalEmptyError() : base(typeof(LocalEmptyError).FullName!, "Local (part before @)")
    {
    }
}