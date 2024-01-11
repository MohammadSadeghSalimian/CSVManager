using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class SetDatabaseFileHandler : IRequestHandler<SetDataBaseFileQuery, FileInfo?>
{
    private readonly ICommonDialogBuilder _dialogBuilder;
    public SetDatabaseFileHandler(ICommonDialogBuilder dialogBuilder)
    {
        _dialogBuilder = dialogBuilder;
    }
    public async Task<FileInfo?> Handle(SetDataBaseFileQuery request, CancellationToken cancellationToken)
    {
        var f = _dialogBuilder.GetDialog();
        f.SetFilters("SQLite database file =|.db");
        f.Title = "Select the SQL Database file";
        if (f.SaveFileDialog(out var fileName))
        {
            return await Task.FromResult(new FileInfo(fileName));
        }
        else
        {
            return null;
        }
    }
}