using System.Collections.Generic;
using CsvManager.Core.Models;
using MediatR;

namespace CsvManager.Queries
{
    public  record GetListOfCsvFilesQuery:IRequest<IReadOnlyList<CsvFile>?>
    {
    }
}
