using MediatR;
using Portfoliosis.Core.Entities;
using Portfoliosis.Core.Errors;
using Portfoliosis.Core.Repositories;
using Portfoliosis.Core.ValueObjects;

namespace Portfoliosis.Application.Commands;

public record SubmitMessageCommand(string Name, string Email, string Text) : IRequest<Result>
{

    public class Handler : IRequestHandler<SubmitMessageCommand, Result>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(SubmitMessageCommand request, CancellationToken cancellationToken)
        {
            (string name, string emailString, string text) = request;

            Result<MyEmailAddress> emailCreationResult = MyEmailAddress.TryCreate(emailString);

            if (emailCreationResult.IsFailure) return emailCreationResult;

            Result<Message> messageCreationResult = Message.TryCreate(name, emailCreationResult.Value, text);

            if (messageCreationResult.IsFailure) return messageCreationResult;

            _messageRepository.Add(messageCreationResult.Value);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Result.Success();
        }
    }
}
