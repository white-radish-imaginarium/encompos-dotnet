using System;
using System.ComponentModel.DataAnnotations;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class PromotionModel
{
    [Required]
    public long Id { get; set; }
    public Optional<string> Name { get; set; }
    public Optional<string> Code { get; set; }
    public Optional<string> Type { get; set; }

    public Optional<DateTimeOffset> Begins { get; set; }
    public Optional<DateTimeOffset> Ends { get; set; }

    public Optional<TimeSpan> TimeOn { get; set; }
    public Optional<TimeSpan> TimeOff { get; set; }

    public Optional<string> DaysOn { get; set; }
}
