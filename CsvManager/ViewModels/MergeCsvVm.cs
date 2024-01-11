using System;
using System.Linq;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;
namespace CsvManager.ViewModels;
public sealed class MergeCsvVm : BaseViewModelWithProgress
{
    public MergeCsvVm( IKeyContainer keyContainer, IMediator mediator,IMessageUnit messageUnit) : base( keyContainer, mediator,messageUnit)
    {
        SetupCommands();
    }
    protected override void SetupCommands()
    {
        MergeCmd = ReactiveCommand.CreateFromTask(MergeCsv);
    }
    public ReactiveCommand<Unit, Unit>? MergeCmd { get; private set; }
    private async Task MergeCsv()
    {
        try
        {
            var csvFiles = await Mediator.Send(new GetListOfCsvFilesQuery());
            if (csvFiles==null || !csvFiles.Any())
            {
                return;
            }
            var savedFile = await Mediator.Send(new GetOpenFileQuery("Csv files |.csv","Please select csv files"));
            if (savedFile==null)
            {
                return;
            }
            var progress = this.Progress;
            if (progress != null)
            {
                var a = await Mediator.Send(new MergeCsvFilesCommands(csvFiles, savedFile, progress));
            }
        }
        catch (Exception e)
        {
            await Mediator.Send(new ShowExceptionErrorCommand(e));
        }
    }
}