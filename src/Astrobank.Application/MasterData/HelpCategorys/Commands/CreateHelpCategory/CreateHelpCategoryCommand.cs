namespace Astrobank.Application.MasterData.HelpCategorys.Commands.CreateHelpCategory;
public class CreateHelpCategoryCommand {
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
