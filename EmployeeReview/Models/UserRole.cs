using System;
using System.Collections.Generic;

namespace EmployeeReview.Models;

public partial class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual UserInfo User { get; set; } = null!;
}
