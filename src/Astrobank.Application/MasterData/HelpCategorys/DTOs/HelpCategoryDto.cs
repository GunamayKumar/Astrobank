namespace Astrobank.Application.MasterData.HelpCategorys.DTOs;
public class HelpCategoryDto {
    public int HelpCategoryID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
