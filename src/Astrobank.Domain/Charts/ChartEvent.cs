using Astrobank.Domain.Common;
using Astrobank.Domain.MasterData;
namespace Astrobank.Domain.Charts;
public class ChartEvent : BaseEntity {
    public int ChartEventID { get; init; }
    public int ChartID { get; init; }
    public int EventTypeID { get; init; }
    public required string EventDate { get; set; }
    public bool ApproximateDate { get; set; }
    public string? Description { get; set; }
    public string? ReferenceSource { get; set; }
    public Chart? Chart { get; private set; }
    public EventType? EventType { get; private set; }
}
