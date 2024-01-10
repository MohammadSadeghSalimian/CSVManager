using MediatR;

namespace CsvManager.Commands;

public record ShowInformationCommand(string Information) : IRequest<Unit>
{

}