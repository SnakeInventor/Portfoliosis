using MediatR;
using Microsoft.AspNetCore.Mvc;
using Portfoliosis.Application.Commands;
using Portfoliosis.Core.Errors;

namespace Portfoliosis.Api.Controllers;


[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private ISender _sender;

    public MessagesController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost("submit")]
    public async Task<IResult> Submit(SubmitMessageCommand command)
    {
        var result = await _sender.Send(command);

        return result.IsSuccess ? Results.Ok() : result.ToProblemDetails();
    }
}
