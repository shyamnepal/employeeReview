using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeReview.Models;

public partial class Category
{
    public Guid? CategoryId { get; set; }

    [Required(ErrorMessage ="Category Name is required")]
    public string CategoryName { get; set; } = null!;

    [Required(ErrorMessage = "Category Total Marks is required")]
    public int CategoryMarks { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    [NotMapped]
    public Review? employee { get; set; }
   // public string SkillRating { get; set; } = null!;
    //public EmployeeCategory EInformation { get; set; }
}
