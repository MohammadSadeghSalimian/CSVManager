using System.IO;
using MediatR;

namespace CsvManager.Queries;

public record GetOpenFileQuery(string Filter,string Title) : IRequest<FileInfo?>
{
    
}