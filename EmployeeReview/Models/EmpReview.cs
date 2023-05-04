using System;
using System.Collections.Generic;

namespace EmployeeReview.Models;

public partial class EmpReview
{
    public Guid ReviewId { get; set; }
    public Guid CategoryId { get; set; }
}
