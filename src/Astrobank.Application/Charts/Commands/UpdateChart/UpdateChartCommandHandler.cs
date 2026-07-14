using Astrobank.Application.Common.CQRS;
using Astrobank.Application.Interfaces.Repositories;
using Astrobank.Domain.Charts;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
namespace Astrobank.Application.Charts.Commands.UpdateChart;
public class UpdateChartCommandHandler : ICommandHandler<UpdateChartCommand> {
    private readonly IChartRepository _repository;
    public UpdateChartCommandHandler(IChartRepository repository) => _repository = repository;
    public async Task HandleAsync(UpdateChartCommand command, CancellationToken cancellationToken = default) {
        var chart = await _repository.GetByIdAsync(command.ChartID, cancellationToken);
        if (chart == null) throw new KeyNotFoundException($"Chart {command.ChartID} not found.");
        if (chart.UserID != command.UserID) throw new UnauthorizedAccessException("Only the chart owner can edit a chart.");
        var oldValues = new { chart.Name, chart.Alias, chart.BirthDate, chart.BirthTime, chart.BirthPlace, chart.Latitude, chart.Longitude, chart.Timezone, chart.Gender, chart.Description, chart.ChartTypeID, chart.ChartPermissionID, chart.CountryID, chart.HelpCategoryID, chart.AskingForHelp, chart.ChartStatus };
        chart.Name = command.Name; chart.Alias = command.Alias; chart.BirthDate = command.BirthDate; chart.BirthTime = command.BirthTime; chart.BirthPlace = command.BirthPlace; chart.Latitude = command.Latitude; chart.Longitude = command.Longitude; chart.Timezone = command.Timezone; chart.Gender = command.Gender; chart.Description = command.Description; chart.ChartTypeID = command.ChartTypeID; chart.ChartPermissionID = command.ChartPermissionID; chart.CountryID = command.CountryID; chart.HelpCategoryID = command.HelpCategoryID; chart.AskingForHelp = command.AskingForHelp; chart.ChartStatus = command.ChartStatus;
        chart.MarkModified();
        var newValues = new { chart.Name, chart.Alias, chart.BirthDate, chart.BirthTime, chart.BirthPlace, chart.Latitude, chart.Longitude, chart.Timezone, chart.Gender, chart.Description, chart.ChartTypeID, chart.ChartPermissionID, chart.CountryID, chart.HelpCategoryID, chart.AskingForHelp, chart.ChartStatus };
        var auditLog = new ChartAuditLog { ChartID = chart.ChartID, ModifiedByUserID = command.UserID, Action = "Updated", OldValuesJson = JsonSerializer.Serialize(oldValues), NewValuesJson = JsonSerializer.Serialize(newValues) };
        auditLog.SetCreatedOn(DateTime.UtcNow);
        await _repository.UpdateAsync(chart, cancellationToken);
    }
}
