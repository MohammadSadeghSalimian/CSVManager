using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class ShowInformationHandler : IRequestHandler<ShowInformationCommand, Unit>
{
    private readonly IMessageUnit _messageUnit;

    public ShowInformationHandler(IMessageUnit messageUnit)
    {
        _messageUnit = messageUnit;
    }
    public Task<Unit> Handle(ShowInformationCommand request, CancellationToken cancellationToken)
    {
        _messageUnit.InformationMessage(request.Information);
        return Unit.Task;
    }
}