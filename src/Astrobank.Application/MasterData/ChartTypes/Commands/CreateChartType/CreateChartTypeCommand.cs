namespace Astrobank.Application.MasterData.ChartTypes.Commands.CreateChartType;
public class CreateChartTypeCommand {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
