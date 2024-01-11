using System.IO;
using MediatR;

namespace CsvManager.Queries;

public record SetDataBaseFileQuery : IRequest<FileInfo?>;