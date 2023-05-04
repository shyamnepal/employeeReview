using System;
using System.Collections.Generic;

namespace EmployeeReview.Models;

public partial class EmployeeUser
{
    public Guid EmployeeId { get; set; }

    public string JobTitle { get; set; } = null!;

    public DateTime DateOfAppointment { get; set; }

    public Guid UserId { get; set; }

    public virtual UserInfo User { get; set; } = null!;
}
