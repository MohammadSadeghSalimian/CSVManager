using CsvManager.Core.Services;
using MediatR;

namespace CsvManager.Queries;

public record GetDataSettings : IRequest<IDataSettings>
{

}