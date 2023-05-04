using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeReview.Models;

public partial class UserInfo
{
    public Guid? UserId { get; set; }

    [Required(ErrorMessage ="First Name is required")]
    public string FirstName { get; set; } = null!;
    public string? UserName { get; set; } = null;
    public string? PasswordHash { get; set; } = null;
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Date Of Birth Name is required")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Gender is required")]
    public string Gender { get; set; } = null!;
    [Required(ErrorMessage = "Marital Status is required")]
    public string MaritalStatus { get; set; } = null!;
    [Required(ErrorMessage = "Religion is required")]
    public string? Religion { get; set; }
    [Required(ErrorMessage = "Blood Group is required")]
    public string? BloodGroup { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public int? Status { get; set; }





    public virtual ICollection<CantactInfo> CantactInfos { get; set; } = new List<CantactInfo>();

    public virtual ICollection<DocumentInfo> DocumentInfos { get; set; } = new List<DocumentInfo>();

    public virtual ICollection<EmployeeUser> EmployeeUsers { get; set; } = new List<EmployeeUser>();
}
