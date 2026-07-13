namespace Astrobank.Application.MasterData.Countries.DTOs;
public class CountryDto {
    public int CountryID { get; set; }
    public string CountryName { get; set; } = string.Empty;
    public string ISOCode2 { get; set; } = string.Empty;
    public string ISOCode3 { get; set; } = string.Empty;
    public string? PhoneCode { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
