using Astrobank.Domain.Users.Enums;
using Astrobank.Domain.Charts.Enums;
using System;
namespace Astrobank.Application.Charts.Commands.UpdateChart;
public class UpdateChartCommand {
    public int ChartID { get; set; }
    public int UserID { get; set; }
    public int ChartTypeID { get; set; }
    public int ChartPermissionID { get; set; }
    public int CountryID { get; set; }
    public int? HelpCategoryID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Alias { get; set; }
    public DateOnly BirthDate { get; set; }
    public TimeOnly BirthTime { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public Gender? Gender { get; set; }
    public bool AskingForHelp { get; set; }
    public string? Description { get; set; }
    public ChartStatus ChartStatus { get; set; }
}
