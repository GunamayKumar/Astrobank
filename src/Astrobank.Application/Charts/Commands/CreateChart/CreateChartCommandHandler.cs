using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Charts;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Astrobank.Application.Charts.Commands.CreateChart;
public class CreateChartCommandHandler : ICommandHandler<CreateChartCommand> {
    private readonly IChartRepository _repository;
    public CreateChartCommandHandler(IChartRepository repository) => _repository = repository;
    public async Task HandleAsync(CreateChartCommand command, CancellationToken cancellationToken = default) {
        var chart = new Chart {
            UserID = command.UserID, ChartTypeID = command.ChartTypeID, ChartPermissionID = command.ChartPermissionID,
            CountryID = command.CountryID, Name = command.Name, Alias = command.Alias, BirthDate = command.BirthDate,
            BirthTime = command.BirthTime, BirthPlace = command.BirthPlace, Latitude = command.Latitude, Longitude = command.Longitude,
            Timezone = command.Timezone, Gender = command.Gender, AskingForHelp = command.AskingForHelp, Description = command.Description, ChartStatus = command.ChartStatus
        };
        chart.SetCreatedOn(DateTime.UtcNow);
        await _repository.AddAsync(chart, cancellationToken);
    }
}
