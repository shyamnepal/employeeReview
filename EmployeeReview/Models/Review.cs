using System;
using System.Collections.Generic;

namespace EmployeeReview.Models;

public partial class Review
{
    public Guid ReviewId { get; set; }
    public Guid UserId { get; set; }

    public Guid? CategoryId { get; set; }

    public string SkillRating { get; set; } = null!;


    public DateTime? ReviewDate { get; set; }

    public string? ReviewBy { get; set; }
    public string Comment { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    //public Category Cat { get; set; }
}
