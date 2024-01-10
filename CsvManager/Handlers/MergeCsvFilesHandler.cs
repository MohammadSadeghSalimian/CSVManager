using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvManager.Commands;
using CsvManager.Core.Statics;
using MediatR;

namespace CsvManager.Handlers;

public class MergeCsvFilesHandler : IRequestHandler<MergeCsvFilesCommands, Unit>
{
    public async Task<Unit> Handle(MergeCsvFilesCommands request, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            Utilities.MergeCsvFiles(request.Files.Select(x => x.FileInfo.FullName).ToList(), request.SaveAddress.FullName, request.Progress);
        }, cancellationToken);
        return Unit.Value;
    }
}