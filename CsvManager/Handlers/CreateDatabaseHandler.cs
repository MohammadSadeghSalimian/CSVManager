using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Core.Services;
using MediatR;

namespace CsvManager.Handlers;

public class CreateDatabaseHandler : IRequestHandler<CreateDatabaseFileCommand, Unit>
{
    private IDatabaseManager _dm;

    public CreateDatabaseHandler(IDatabaseManager dm)
    {
        _dm = dm;
    }

    public async Task<Unit> Handle(CreateDatabaseFileCommand request, CancellationToken cancellationToken)
    {
        IDatabaseManager dm = new DatabaseLiteManager(request.CsvFile, request.DatabaseFile, request.IgnoredColumns);
        await Task.Run(() =>
        {
            dm.CreateDatabase(request.TableName);
            dm.BulkInsert(request.ReferenceTime);
        }, cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}