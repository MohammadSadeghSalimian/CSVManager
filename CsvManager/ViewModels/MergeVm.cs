using System;
using System.Linq;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Core.Statics;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Unit = System.Reactive.Unit;

namespace CsvManager.ViewModels;

public class MergeVm : BaseViewModel
{



    public MergeVm( IKeyContainer keyContainer, IMediator mediator) : base( keyContainer, mediator)
    {


        SetupCommands();
    }

    [Reactive] public double Progress { get; private set; }
    [Reactive] public string? IgnoredColumns { get; set; }
    [Reactive] public DateTime? PickedDateTime { get; set; }
    [Reactive] public string TableName { get; set; }
   
    public ReactiveCommand<Unit, Unit>? MergeCmd { get; private set; }
   
    public ReactiveCommand<Unit, Unit>? ConvertToDatabaseCmd { get; private set; }
    protected sealed override void SetupCommands()
    {
        MergeCmd = ReactiveCommand.CreateFromTask(MergeCsv);
    }

    private async Task MergeCsv()
    {

        try
        {
            var csvFiles = await Mediator.Send(new GetListOfCsvFilesQuery());
            if (csvFiles==null || !csvFiles.Any())
            {
                return;
            }
            var savedFile = await Mediator.Send(new GetOpenFileQuery());
            if (savedFile==null)
            {
                return;
            }
            var progress = new Progress<double>(x =>
            {
                Progress = x;
            });
            var a = await Mediator.Send(new MergeCsvFilesCommands(csvFiles, savedFile, progress));
        }
        catch (Exception e)
        {
            await Mediator.Send(new ShowExceptionErrorCommand(e));
        }
    }

    private async Task Convert()
    {
        try
        {
            var csvFile = await Mediator.Send(new GetBigCsvFileQuery());
            var databaseFile= await Mediator.Send(new CreateDataBaseCommand());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}