using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeReview.Models;

public partial class DocumentInfo
{
    [Required(ErrorMessage ="Citizenship Number is required")]
    public string CitizenshipNumber { get; set; } = null!;
    public Guid UserId { get; set; }
    [Required(ErrorMessage = "Citizenship Issue Place is required")]
    public string CitizenshipIssuePlace { get; set; } = null!;

    public string? PanNumber { get; set; }

    public string? Citnumber { get; set; }

    public string? Pfnumber { get; set; }

    public string? Sffnumber { get; set; }

    [Required(ErrorMessage = "Academic Qualification is required")]
    public string AcademicQualification { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
    public Guid? DocumnetId { get; set; }

    [Required(ErrorMessage = "Select Document Type is required")]
    public string SelectDocType { get; set; } = null!;

    public string? DocName { get; set; }

    [NotMapped]
    public IFormFile Doc { get; set; }

    public virtual UserInfo? User { get; set; } = null;
}
