using Astrobank.Domain.Common;
namespace Astrobank.Domain.Charts;
public class ChartFile : BaseEntity {
    public int ChartFileID { get; init; }
    public int ChartID { get; init; }
    public required string OriginalFileName { get; set; }
    public required string StoredFileName { get; set; }
    public required string RelativePath { get; set; }
    public required string MimeType { get; set; }
    public long FileSize { get; set; }
    public string? Description { get; set; }
    public Chart? Chart { get; private set; }
}
