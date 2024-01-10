using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvManager.Core.Models;
using MediatR;

namespace CsvManager.Queries
{
    public  record GetListOfCsvFilesQuery:IRequest<IReadOnlyList<CsvFile>?>
    {
    }
}
