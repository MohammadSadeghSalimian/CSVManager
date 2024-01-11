using System;
using System.Collections.Generic;
using System.IO;
using CsvManager.Core.Models;
using MediatR;

namespace CsvManager.Commands
{
    public record MergeCsvFilesCommands(IReadOnlyList<CsvFile> Files,FileInfo SaveAddress,IProgress<double> Progress) : IRequest<Unit>;

}
