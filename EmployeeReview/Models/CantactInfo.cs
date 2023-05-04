using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeReview.Models;

public partial class CantactInfo
{
    public Guid? ContactId { get; set; }

    public Guid UserId { get; set; }
    [Required(ErrorMessage ="Permanent Address is required")]
    public string PermanentAddress { get; set; } = null!;
    [Required(ErrorMessage = "Permanent Country is required")]
    public string PermanentCountry { get; set; } = null!;

    [Required(ErrorMessage = "Permanent State is required")]
    public string PermanentState { get; set; } = null!;

    [Required(ErrorMessage = "Temporary Address is required")]
    public string? TemporaryAddress { get; set; }

    [Required(ErrorMessage = "Temporary Country is required")]
    public string? TemporaryCountry { get; set; }

    [Required(ErrorMessage = "Temporary State is required")]
    public string? TemporaryState { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = null!;

    public string? AlternativeEmail { get; set; }

    [Required(ErrorMessage ="Contact Number is required")]
    public string ContactNumber { get; set; } = null!;

    public string? EmergencyContactNumber { get; set; }

    public string? ContactName { get; set; }

    public string? ContactRelation { get; set; }

    public string? Facebook { get; set; }

    public string? Skype { get; set; }

    public string? LinkedIn { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual UserInfo? User { get; set; } = null;
}
