using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class GetBigCsvFileHandler : IRequestHandler<GetBigCsvFileQuery, FileInfo?>
{
    private readonly ICommonDialogBuilder _dialogBuilder;

    public GetBigCsvFileHandler(ICommonDialogBuilder dialogBuilder)
    {
        _dialogBuilder = dialogBuilder;
    }

    public async Task<FileInfo?> Handle(GetBigCsvFileQuery request, CancellationToken cancellationToken)
    {
        var op = _dialogBuilder.GetDialog();
        op.SetFilters("Csv file|csv");
        op.Title = "Select the CSV file";
        if (!op.OpenFileDialog(out var files))
        {
            return null;
        }

        return await Task.FromResult(new FileInfo(files));
    }
}