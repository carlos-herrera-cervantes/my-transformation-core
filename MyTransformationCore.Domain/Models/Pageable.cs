using Microsoft.AspNetCore.Mvc;

using MyTransformationCore.Domain.Configs;

namespace MyTransformationCore.Domain.Models;

public class Pageable
{
    #region snippet_Properties

    [FromQuery(Name = "page")]
    public int Page { get; set; } = 0;

    [FromQuery(Name = "page_size")]
    public int PageSize { get; set; } = 10;

    [FromQuery(Name = "from")]
    public DateTime? From { get; set; }

    [FromQuery(Name = "to")]
    public DateTime? To { get; set; }

    [FromQuery(Name = "no_exercise")]
    public bool NoExercise { get; set; } = false;

    #endregion

    #region snippet_Constructors

    public Pageable()
    {
        var now = DateTime.UtcNow;
        var thirtyDaysAgo = now.AddDays(-30);
        var timezone = TimeZoneInfo.FindSystemTimeZoneById(ApiConfig.Timezone);

        this.From = TimeZoneInfo.ConvertTimeFromUtc(thirtyDaysAgo, timezone);
        this.To = TimeZoneInfo.ConvertTimeFromUtc(now, timezone);
    }

    #endregion
}
