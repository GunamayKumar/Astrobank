namespace Astrobank.Application.MasterData.TagCategorys.Commands.CreateTagCategory;
public class CreateTagCategoryCommand {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
