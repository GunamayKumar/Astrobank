namespace Astrobank.Application.MasterData.ChartTypes.Commands.UpdateChartType;
public class UpdateChartTypeCommand {
    public int ChartTypeID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
