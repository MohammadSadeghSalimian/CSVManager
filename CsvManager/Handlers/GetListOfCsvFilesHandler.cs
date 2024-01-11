using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.Models;
using CsvManager.Core.Statics;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers
{
    public class GetListOfCsvFilesHandler : IRequestHandler<GetListOfCsvFilesQuery, IReadOnlyList<CsvFile>?>
    {
        private readonly ICommonDialogBuilder _dialogBuilder;

        public GetListOfCsvFilesHandler(ICommonDialogBuilder dialogBuilder)
        {
            _dialogBuilder = dialogBuilder;
        }
        public async Task<IReadOnlyList<CsvFile>?> Handle(GetListOfCsvFilesQuery request, CancellationToken cancellationToken)
        {
            var op = _dialogBuilder.GetDialog();
            op.SetFilters("Csv file|csv");
            op.Title = "Select the CSV files";
            if (!op.OpenManyFileDialog(out var files))
            {
                return null;
            }



            var csvFiles = await Task.Run(() => Utilities.SortCsvFiles(files.ToList()), cancellationToken);

            return csvFiles;

        }
    }
}
