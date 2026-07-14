using Astrobank.Domain.Charts.Enums;
using System;
namespace Astrobank.Application.Charts.DTOs;
public class ChartDto {
    public int ChartID { get; set; }
    public int UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int ChartTypeID { get; set; }
    public string ChartTypeName { get; set; } = string.Empty;
    public int ChartPermissionID { get; set; }
    public string ChartPermissionName { get; set; } = string.Empty;
    public int CountryID { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Alias { get; set; }
    public DateOnly BirthDate { get; set; }
    public TimeOnly BirthTime { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    public bool AskingForHelp { get; set; }
    public ChartStatus ChartStatus { get; set; }
    public DateTime CreatedOn { get; set; }
}
