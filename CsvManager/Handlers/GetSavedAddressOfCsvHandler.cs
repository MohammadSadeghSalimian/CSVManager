using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class GetSavedAddressOfCsvHandler : IRequestHandler<GetOpenFileQuery,FileInfo?>
{
    private readonly ICommonDialogBuilder _dialogBuilder;
    public GetSavedAddressOfCsvHandler(ICommonDialogBuilder dialogBuilder)
    {
        _dialogBuilder = dialogBuilder;
    }

    public async Task<FileInfo?> Handle(GetOpenFileQuery request, CancellationToken cancellationToken)
    {
        var sp = _dialogBuilder.GetDialog();
        sp.SetFilters("Csv file|csv");
        sp.Title = "Save the CSv file";
        if (!sp.SaveFileDialog(out var saveFileName))
        {
            return null;

        }

        return await Task.FromResult(new FileInfo(saveFileName));
    }
}