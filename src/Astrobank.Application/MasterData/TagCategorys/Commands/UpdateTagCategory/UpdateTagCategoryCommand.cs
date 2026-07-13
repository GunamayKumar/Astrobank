namespace Astrobank.Application.MasterData.TagCategorys.Commands.UpdateTagCategory;
public class UpdateTagCategoryCommand {
    public int TagCategoryID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
