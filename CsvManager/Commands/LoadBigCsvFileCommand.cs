using System.IO;
using MediatR;

namespace CsvManager.Commands;

public record LoadBigCsvFileCommand() : IRequest<FileInfo?>
{

}