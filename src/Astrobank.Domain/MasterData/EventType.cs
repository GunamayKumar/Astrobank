using Astrobank.Domain.Common;
namespace Astrobank.Domain.MasterData;
public class EventType : BaseEntity {
    public int EventTypeID { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
