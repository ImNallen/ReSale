using ReSale.Domain.Common;

namespace ReSale.Domain.Employees;

public static class EmployeeErrors
{
    public static readonly Error NotFound = Error.NotFound("Employee.NotFound", "Employee not found.");
}
