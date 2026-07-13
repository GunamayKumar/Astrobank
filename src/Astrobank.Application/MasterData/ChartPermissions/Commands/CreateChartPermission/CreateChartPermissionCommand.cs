namespace Astrobank.Application.MasterData.ChartPermissions.Commands.CreateChartPermission;
public class CreateChartPermissionCommand {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
