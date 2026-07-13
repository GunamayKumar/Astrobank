using Astrobank.Domain.Common;
using Astrobank.Domain.Users;
namespace Astrobank.Domain.Charts;
public class ChartAuditLog : BaseEntity {
    public int ChartAuditLogID { get; init; }
    public int ChartID { get; init; }
    public int ModifiedByUserID { get; init; }
    public required string Action { get; set; }
    public string? ChangedFieldsJson { get; set; }
    public string? OldValuesJson { get; set; }
    public string? NewValuesJson { get; set; }
    public Chart? Chart { get; private set; }
    public User? ModifiedByUser { get; private set; }
}
