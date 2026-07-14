using Astrobank.Domain.Common;
using Astrobank.Domain.MasterData;
namespace Astrobank.Domain.Charts;
public class ChartTag : BaseEntity {
    public int ChartTagID { get; init; }
    public int ChartID { get; init; }
    public int TagID { get; init; }
    public Chart? Chart { get; private set; }
    public Tag? Tag { get; private set; }
}
