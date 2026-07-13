using Astrobank.Domain.Common;
using Astrobank.Domain.MasterData;
namespace Astrobank.Domain.Charts;
public class ChartImage : BaseEntity {
    public int ChartImageID { get; init; }
    public int ChartID { get; init; }
    public int ChartImageTypeID { get; init; }
    public required string OriginalFileName { get; set; }
    public required string StoredFileName { get; set; }
    public required string RelativePath { get; set; }
    public required string MimeType { get; set; }
    public long FileSize { get; set; }
    public int DisplayOrder { get; set; }
    public Chart? Chart { get; private set; }
    public ChartImageType? ChartImageType { get; private set; }
}
