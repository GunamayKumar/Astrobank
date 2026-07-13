namespace Astrobank.Application.MasterData.ChartPermissions.DTOs;
public class ChartPermissionDto {
    public int ChartPermissionID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
