using Portfoliosis.Core.Errors;
using Portfoliosis.Core.Errors.Message;
using Portfoliosis.Core.ValueObjects;

namespace Portfoliosis.Core.Entities;

public class Message : EntityBase
{
    public const int MaxNameLength = 255;
    public const int MaxTextLength = 4000;
    
    public string Name { get; private set; }
    public MyEmailAddress Email { get; private set; }
    public string Text { get; private set; }
    public DateTime TimeStamp { get; private set; }

    private Message(Guid id, string name, MyEmailAddress email, string text, DateTime timeStamp) : base(id)
    {
        Email = email;
        Text = text;
        Name = name;
        TimeStamp = timeStamp;
    }

    public static Result<Message> TryCreate(string name, MyEmailAddress email, string text)
    {
        if (name.Length == 0)
        {
            return Result.Failure<Message>(new NameEmptyError());
        }

        if (name.Length > MaxNameLength)
        {
            return Result.Failure<Message>(new NameTooLongError(MaxNameLength, name.Length));
        }

        if (text.Length == 0)
        {
            return Result.Failure<Message>(new TextEmptyError());
        }

        if (text.Length > MaxTextLength)
        {
            return Result.Failure<Message>(new TextTooLongError(MaxTextLength, text.Length));
        }
        
        return new Message(Guid.NewGuid(), name, email, text, DateTime.UtcNow);
    }
}
