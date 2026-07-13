using Astrobank.Domain.Common;
namespace Astrobank.Domain.MasterData;
public class Tag : BaseEntity {
    public int TagID { get; init; }
    public int TagCategoryID { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public TagCategory? TagCategory { get; private set; }
}
