using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class TextErrorHandler : IRequestHandler<ShowTextErrorCommand, Unit>
{
    private readonly IMessageUnit _messageUnit;

    public TextErrorHandler(IMessageUnit messageUnit)
    {
        _messageUnit = messageUnit;
    }
    public Task<Unit> Handle(ShowTextErrorCommand request, CancellationToken cancellationToken)
    {
        _messageUnit.ErrorMessage(request.ErrorText);
        return Unit.Task;
    }
}

public class LoadBigCsvHandler : IRequestHandler<LoadBigCsvFileCommand, FileInfo?>
{
    private readonly ICommonDialogBuilder _dialogBuilder;

    public LoadBigCsvHandler(ICommonDialogBuilder dialogBuilder)
    {
        _dialogBuilder = dialogBuilder;
    }
    public async Task<FileInfo?> Handle(LoadBigCsvFileCommand request, CancellationToken cancellationToken)
    {
        var ff= _dialogBuilder.GetDialog();
        ff.SetFilters("Csv files|csv");
        ff.Title = "Please select the big csv file";
        if (!ff.OpenFileDialog(out var fileName))
        {
            return null;
        }

        return await Task.FromResult(new FileInfo(fileName));
    }
}