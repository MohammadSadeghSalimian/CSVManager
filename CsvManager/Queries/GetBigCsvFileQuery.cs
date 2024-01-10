using System.IO;
using MediatR;

namespace CsvManager.Queries;

public record GetBigCsvFileQuery : IRequest<FileInfo?>
{

}