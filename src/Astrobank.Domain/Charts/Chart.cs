using Astrobank.Domain.Charts.Enums;
using Astrobank.Domain.Common;
using Astrobank.Domain.MasterData;
using Astrobank.Domain.Users;
using Astrobank.Domain.Users.Enums;
using System;
using System.Collections.Generic;

namespace Astrobank.Domain.Charts;
public class Chart : BaseEntity, ISoftDelete {
    public int ChartID { get; init; }
    public int UserID { get; init; }
    public int ChartTypeID { get; init; }
    public int ChartPermissionID { get; init; }
    public int CountryID { get; init; }
    public int? HelpCategoryID { get; init; }

    public required string Name { get; set; }
    public string? Alias { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required TimeOnly BirthTime { get; set; }
    public required string BirthPlace { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public required string Timezone { get; set; }

    public Gender? Gender { get; set; }
    public bool AskingForHelp { get; set; }
    public DateTime? HelpExpiresOn { get; set; }
    public string? Description { get; set; }
    public ChartStatus ChartStatus { get; set; }
    public bool IsDeleted { get; set; }

    public User? User { get; private set; }
    public ChartType? ChartType { get; private set; }
    public ChartPermission? ChartPermission { get; private set; }
    public Country? Country { get; private set; }
    public HelpCategory? HelpCategory { get; private set; }

    public ICollection<ChartImage> ChartImages { get; private set; } = new List<ChartImage>();
    public ICollection<ChartFile> ChartFiles { get; private set; } = new List<ChartFile>();
    public ICollection<ChartEvent> ChartEvents { get; private set; } = new List<ChartEvent>();
    public ICollection<ChartTag> ChartTags { get; private set; } = new List<ChartTag>();
    public ICollection<ChartAccess> ChartAccesses { get; private set; } = new List<ChartAccess>();
}
