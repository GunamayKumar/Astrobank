using Astrobank.Domain.Common;
using Astrobank.Domain.Users;
using System;
namespace Astrobank.Domain.Charts;
public class ChartAccess : BaseEntity {
    public int ChartAccessID { get; init; }
    public int ChartID { get; init; }
    public int UserID { get; init; }
    public DateTime GrantedOn { get; set; }
    public Chart? Chart { get; private set; }
    public User? User { get; private set; }
}
