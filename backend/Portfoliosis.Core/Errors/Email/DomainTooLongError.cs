using Portfoliosis.Core.Errors.Shared;

namespace Portfoliosis.Core.Errors.Email;

public sealed class DomainTooLongError : TooLongError
{
    public DomainTooLongError(int expectedLength, int actualLength) : base(typeof(DomainTooLongError).FullName!, "Domain (part after @)", expectedLength, actualLength)
    {
    }
}