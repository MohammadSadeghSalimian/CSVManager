using System;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Core.Models;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class TextErrorHandler : IRequestHandler<ShowTextErrorCommand, Unit>
{
    private readonly IMessageUnit _messageUnit;

    public TextErrorHandler(IMessageUnit messageUnit)
    {
        _messageUnit = messageUnit;
    }
    public Task<Unit> Handle(ShowTextErrorCommand request, CancellationToken cancellationToken)
    {
        _messageUnit.ErrorMessage(request.ErrorText);
        return Unit.Task;
    }
}