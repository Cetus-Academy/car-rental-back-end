using System.ComponentModel.DataAnnotations;

namespace NetCoreTemplate.Domain.Reports.Enums;

public enum ReportPriority : short
{
    [Display(Name = "Niski")]
    Low = 0,
    [Display(Name = "Wysoki")]
    High = 2,
    [Display(Name = "Bardzo wysoki")]
    ExtremelyHigh = 6
}