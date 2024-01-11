using System;
using System.IO;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Core.Services;
using CsvManager.Queries;
using CsvManager.Services;
using Exceptionless.DateTimeExtensions;
using MediatR;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Unit = System.Reactive.Unit;

namespace CsvManager.ViewModels;

public sealed class SqLiteDatabaseVm : BaseViewModelWithProgress
{
    public SqLiteDatabaseVm(IKeyContainer keyContainer, IMediator mediator, IMessageUnit messageUnit) : base(keyContainer, mediator,messageUnit)
    {
        SetupCommands();
        SetupProperties();
    }

   

    [Reactive] public string? IgnoredColumns { get; set; }
    [Reactive] public DateTime? PickedDateTime { get; set; }
    [Reactive] public string? TableName { get; set; }

    public ReactiveCommand<Unit, Unit>? ConvertToDatabaseCmd { get; private set; }
    protected override void SetupProperties()
    {
        this.TableName = "Raw Data";
        this.PickedDateTime=DateTime.Today;

    }
    protected override void SetupCommands()
    {
        ConvertToDatabaseCmd = ReactiveCommand.CreateFromTask(Convert);
    }

    private async Task Convert()
    {
        try
        {
            var csvFile = await Mediator.Send(new GetBigCsvFileQuery());
            if (csvFile is not { Exists: true })
            {
                return;
            }
            var databaseFile = await Mediator.Send(new SetDataBaseFileQuery());

            if (databaseFile == null)
            {
                return;
            }
            if (databaseFile.Exists)
            {
                databaseFile.Directory?.Create();
            }

            var ignored = !string.IsNullOrEmpty(IgnoredColumns) ? IgnoredColumns.Split(',', StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();

            if (string.IsNullOrEmpty(TableName))
            {
                MessageUnit.ErrorMessage("Table name must be set!");
                return;
            }
            if (PickedDateTime == null)
            {

                MessageUnit.ErrorMessage("A reference time must be set!");
                return;
            }

            await Mediator.Send(
                new CreateDatabaseFileCommand(csvFile, databaseFile, TableName, PickedDateTime.Value, ignored));

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}