namespace Astrobank.Application.MasterData.EventTypes.DTOs;
public class EventTypeDto {
    public int EventTypeID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
