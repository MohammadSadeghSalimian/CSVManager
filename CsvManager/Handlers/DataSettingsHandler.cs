using System.Threading;
using System.Threading.Tasks;
using CsvManager.Core.Services;
using CsvManager.Queries;
using CsvManager.Services;
using MediatR;

namespace CsvManager.Handlers;

public class DataSettingsHandler : IRequestHandler<GetDataSettings, IDataSettings>
{
    private readonly ISettings _settings;

    public DataSettingsHandler(ISettings settings)
    {
        _settings = settings;
    }
    public async Task<IDataSettings> Handle(GetDataSettings request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_settings.Data);
    }
}