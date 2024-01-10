using MediatR;

namespace CsvManager.Commands;

public record ShowTextErrorCommand(string ErrorText) : IRequest<Unit>
{
}