using System;
using System.Collections.Generic;
using System.IO;
using MediatR;

namespace CsvManager.Commands;

public record CreateDatabaseFileCommand(FileInfo CsvFile,FileInfo DatabaseFile,string TableName,DateTime ReferenceTime,string[] IgnoredColumns):IRequest<Unit>;