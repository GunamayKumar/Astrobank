using Astrobank.Domain.Common;
namespace Astrobank.Domain.MasterData;
public class ChartImageType : BaseEntity {
    public int ChartImageTypeID { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
