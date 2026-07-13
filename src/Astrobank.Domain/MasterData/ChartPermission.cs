using Astrobank.Domain.Common;
namespace Astrobank.Domain.MasterData;
public class ChartPermission : BaseEntity {
    public int ChartPermissionID { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
