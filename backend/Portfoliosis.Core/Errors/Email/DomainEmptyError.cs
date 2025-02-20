using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public class DomainEmptyError : EmptyError
{
    public DomainEmptyError() : base(typeof(DomainEmptyError).FullName!, "Domain (part after @)")
    {
    }
}