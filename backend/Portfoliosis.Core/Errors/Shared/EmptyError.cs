namespace Portfoliosis.Core.Errors.Shared;

public class EmptyError : Error
{

    public EmptyError(string code, string subject)
        : base(
            code,
            $"{subject} is empty.",
            ErrorType.Validation
        )
    {

    }
 
}
