using System.ComponentModel.DataAnnotations;

namespace NetCoreTemplate.Domain.Reports.Enums;

public enum ReportType : short
{
    [Display(Name = "Powódź")]
    Flood = 2,
    
    [Display(Name = "Podtopienie")]
    Inundation = 4,
    
    [Display(Name = "Powietrzne")]
    Air = 6,
    
    [Display(Name = "Kopciuch")]
    IncompatibleStove = 8,
    
    [Display(Name = "Inne")]
    Others = 10
}